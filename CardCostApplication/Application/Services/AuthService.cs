using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CardCostApplication.Application.Interfaces;
using CardCostApplication.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CardCostApplication.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _users;
		private readonly IConfiguration _config;

		public AuthService(IUserRepository users, IConfiguration config)
		{
			_users = users;
			_config = config;
		}

		public async Task<string?> AuthenticateAsync(string username, string password)
		{
			var user = await _users.FindByUsernameAsync(username);
			if (user is null || user.Password != password)
				return null;

			// Load RSA private key (PKCS#8 PEM)
			var keyPath = _config["Jwt:PrivateKeyPath"]!;
			var pem = await File.ReadAllTextAsync(keyPath);
			using var rsa = RSA.Create();
			rsa.ImportFromPem(pem);

			var creds = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

			var issuer = _config["Jwt:Issuer"];
			var audience = _config["Jwt:Audience"];
			var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMinutes"] ?? "60"));

			var claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Sub, user.Username),
				new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
			};

			// ίδιο με Spring: claim "groups" (ή βάλε "roles" αν προτιμάς)
			if (!string.IsNullOrWhiteSpace(user.Role))
			{
				claims.Add(new("groups", user.Role));
			}

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				notBefore: DateTime.UtcNow,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
