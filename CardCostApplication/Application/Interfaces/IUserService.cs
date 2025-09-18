using CardCostApplication.Domain.Entities;
using System.Threading.Tasks;

namespace CardCostApplication.Application.Interfaces
{
	public interface IUserService
	{
		Task<User?> FindByUsernameAsync(string username);
	}
}
