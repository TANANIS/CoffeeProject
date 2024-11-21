using WebApplication1_1105_TSET_member5.Models;

namespace WebApplication1_1105_TSET_member5.Views.Account
{
    public class HeaderViewModel
    {
        public string OrderId { get; set; } = null!;

        //public int Id { get; set; }

        public string? CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Payment { get; set; }

        public int? Total { get; set; }

        public string? OrderStatus { get; set; }

        public string? ShipStatus { get; set; }


        // 添加每個OrderId對應的產品名稱列表
        public List<DetailViewModel> Orderdetails { get; set; } = new List<DetailViewModel>();
    }
}
