using System.ComponentModel.DataAnnotations;

namespace CardCostApplication.Contracts.Responses
{
	public class ClearingCostRepresentation
	{
		public long Id { get; set; }

		[Required]
		[MinLength(1)]
		public string CountryCode { get; set; } = string.Empty;

		[Required]
		[Range(0, int.MaxValue)]
		public int Cost { get; set; }
	}
}
