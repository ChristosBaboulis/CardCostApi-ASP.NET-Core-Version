using Microsoft.EntityFrameworkCore;
using CardCostApplication.Domain.Entities;

namespace CardCostApplication.Infrastructure.Persistence
{
	public class CardCostDbContext : DbContext
	{
		public CardCostDbContext(DbContextOptions<CardCostDbContext> options)
			: base(options) { }

		public DbSet<ClearingCost> ClearingCosts { get; set; } = null!;
	}
}
