using WebApplication1_1105_TSET_member5.Models;

namespace WebApplication1_1105_TSET_member5.Views.Account
{
	public class headerViewModel
	{
		public string OrderId { get; set; } = null!;

		public int Id { get; set; }

		public string? CustomerId { get; set; }

		public DateTime? OrderDate { get; set; }

		public string? Payment { get; set; }

		public int? Total { get; set; }

		public string? OrderStatus { get; set; }

		public string? ShipStatus { get; set; }

        public List<DetailViewModel> DetailViewModels { get; set; } 
    }
}
