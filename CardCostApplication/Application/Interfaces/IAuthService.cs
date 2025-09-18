using System.Threading.Tasks;

namespace CardCostApplication.Application.Interfaces
{
	public interface IAuthService
	{
		Task<string?> AuthenticateAsync(string username, string password);
	}
}
