using CardCostApplication.Application.Interfaces;
using CardCostApplication.Domain.Entities;
using System.Threading.Tasks;

namespace CardCostApplication.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<User?> FindByUsernameAsync(string username)
		{
			return _userRepository.FindByUsernameAsync(username);
		}
	}
}
