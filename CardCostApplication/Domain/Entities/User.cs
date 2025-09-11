using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCostApplication.Domain.Entities
{
	[Table("users")]
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }       // Primary Key

		[Column("username")]
		[MaxLength(50)]
		[Required]
		public string Username { get; set; } = string.Empty;

		[Column("password")]
		[MaxLength(50)]
		[Required]
		public string Password { get; set; } = string.Empty;

		[Column("role")]
		public string Role { get; set; } = "user"; // Default role is "user"

		public User(string username, string password, string role = "user")
		{
			Username = username;
			Password = password;
			Role = role;
		}
	}
}
