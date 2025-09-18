using CardCostApplication.Domain.Entities;
using System.Threading.Tasks;

namespace CardCostApplication.Application.Interfaces
{
	public interface IClearingCostRepository
	{
		Task<List<ClearingCost>> GetAllAsync();
		Task<ClearingCost?> FindByIdAsync(long id);
		Task<ClearingCost?> FindByCountryCodeAsync(string countryCode);
		void Update(ClearingCost entity);
		Task DeleteByIdAsync(long id);
		Task AddAsync(ClearingCost entity);
		Task SaveChangesAsync();
	}
}
