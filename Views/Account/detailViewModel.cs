namespace WebApplication1_1105_TSET_member5.Views.Account
{
    public class DetailViewModel
    {

        public string? ProductName { get; set; }

        public string OrderId { get; set; } = null!;

        public short OrderItem { get; set; }

        public int Id { get; set; }

        public short? Qty { get; set; }

        public string? Uom { get; set; }

        public short? UnitPrice { get; set; }

        public int? Totle { get; set; }
    }
}
