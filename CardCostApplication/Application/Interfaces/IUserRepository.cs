using CardCostApplication.Domain.Entities;
using System.Threading.Tasks;

namespace CardCostApplication.Application.Interfaces
{
	public interface IUserRepository
	{
		Task<User?> FindByIdAsync(long id);
		Task<User?> FindByUsernameAsync(string username);
		Task AddAsync(User entity);
		Task SaveChangesAsync();
	}
}
