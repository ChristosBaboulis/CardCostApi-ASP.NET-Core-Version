using CardCostApplication.Application.Interfaces;
using CardCostApplication.Contracts.Requests;
using CardCostApplication.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardCostApplication.Controllers
{
	[ApiController]
	[Route("auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] AuthRequest request)
		{
			var token = await _authService.AuthenticateAsync(request.Username, request.Password);

			if (token == null)
				return Unauthorized("Invalid credentials");

			return Ok(new AuthResponse(token));
		}
	}
}
