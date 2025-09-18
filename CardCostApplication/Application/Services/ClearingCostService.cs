using CardCostApplication.Application.Interfaces;
using CardCostApplication.Domain.Entities;

namespace CardCostApplication.Application.Services
{
	public class ClearingCostService : IClearingCostService
	{
		private readonly IClearingCostRepository _repository;
		public ClearingCostService(IClearingCostRepository repository) => _repository = repository;

		public Task<List<ClearingCost>> GetAllAsync() => _repository.GetAllAsync();

		public Task<ClearingCost?> GetByIdAsync(long id) => _repository.FindByIdAsync(id);

		public Task<ClearingCost?> GetByCountryCodeAsync(string countryCode) =>
			_repository.FindByCountryCodeAsync(countryCode.ToUpperInvariant());

		public async Task<ClearingCost> SaveAsync(ClearingCost clearingCost)
		{
			clearingCost.CountryCode = clearingCost.CountryCode.ToUpperInvariant();
			if (clearingCost.Id == 0)
				await _repository.AddAsync(clearingCost);
			else
				_repository.Update(clearingCost);

			await _repository.SaveChangesAsync();
			return clearingCost;
		}

		public async Task DeleteByIdAsync(long id)
		{
			await _repository.DeleteByIdAsync(id);
			await _repository.SaveChangesAsync();
		}

		public async Task<ClearingCost> UpsertByCountryCodeAsync(ClearingCost incoming)
		{
			var code = incoming.CountryCode.ToUpperInvariant();
			incoming.CountryCode = code;

			var existing = await _repository.FindByCountryCodeAsync(code);
			if (existing is not null)
			{
				existing.Cost = incoming.Cost;
				_repository.Update(existing);
				await _repository.SaveChangesAsync();
				return existing;
			}

			await _repository.AddAsync(incoming);
			await _repository.SaveChangesAsync();
			return incoming;
		}
	}
}
