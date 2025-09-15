using CardCostApplication.Application.Interfaces;
using CardCostApplication.Domain.Entities;
using CardCostApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CardCostApplication.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly CardCostDbContext _context;

		public UserRepository(CardCostDbContext context)
		{
			_context = context;
		}

		public async Task<User?> FindByIdAsync(long id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<User?> FindByUsernameAsync(string username)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task AddAsync(User entity)
		{
			await _context.Users.AddAsync(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
