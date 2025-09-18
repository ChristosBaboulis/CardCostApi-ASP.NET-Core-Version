using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCostApplication.Domain.Entities
{
	[Table("clearing_costs")]
	public class ClearingCost
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }       // Primary Key
		
		[Column("country_code")]
		[Required]
		[MaxLength(10)]
		public string CountryCode { get; set; } = string.Empty;

		[Column("cost")]
		[Required]
		public int Cost { get; set; }
	}
}
