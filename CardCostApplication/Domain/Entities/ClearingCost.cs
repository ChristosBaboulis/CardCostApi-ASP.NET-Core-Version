namespace CardCostApplication.Domain.Entities
{
	public class ClearingCost
	{
		public int Id { get; set; }       // Primary Key
		public string Name { get; set; } = string.Empty;
		public decimal Cost { get; set; }
	}
}
