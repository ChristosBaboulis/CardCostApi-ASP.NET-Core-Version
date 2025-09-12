using CardCostApplication.Domain.Entities;
using System.Threading.Tasks;

namespace CardCostApplication.Application.Interfaces
{
	public interface IClearingCostRepository
	{
		Task<ClearingCost?> FindByIdAsync(long id);
		Task<ClearingCost?> FindByCountryCodeAsync(string countryCode);
		Task AddAsync(ClearingCost entity);
		Task SaveChangesAsync();
	}
}
