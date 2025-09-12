using CardCostApplication.Application.Interfaces;
using CardCostApplication.Domain.Entities;
using CardCostApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CardCostApplication.Infrastructure.Repositories
{
	public class ClearingCostRepository : IClearingCostRepository
	{
		private readonly CardCostDbContext _context;

		public ClearingCostRepository(CardCostDbContext context)
		{
			_context = context;
		}

		public async Task<ClearingCost?> FindByIdAsync(long id)
		{
			return await _context.ClearingCosts.FindAsync(id);
		}

		public async Task<ClearingCost?> FindByCountryCodeAsync(string countryCode)
		{
			return await _context.ClearingCosts
				.FirstOrDefaultAsync(c => c.CountryCode == countryCode);
		}

		public async Task AddAsync(ClearingCost entity)
		{
			await _context.ClearingCosts.AddAsync(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
