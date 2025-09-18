using CardCostApplication.Domain.Entities;

namespace CardCostApplication.Application.Interfaces
{
	public interface IClearingCostService
	{
		Task<List<ClearingCost>> GetAllAsync();
		Task<ClearingCost?> GetByIdAsync(long id);
		Task<ClearingCost?> GetByCountryCodeAsync(string countryCode);
		Task<ClearingCost> SaveAsync(ClearingCost clearingCost);
		Task DeleteByIdAsync(long id);
		Task<ClearingCost> UpsertByCountryCodeAsync(ClearingCost incoming);
	}
}
