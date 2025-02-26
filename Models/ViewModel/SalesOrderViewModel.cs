namespace ProductManagementSystem.Models.ViewModel
{
    public class SalesOrderViewModel
    {
        public int HeaderId { get; set; }

        public int? UserId { get; set; }
        public string Username { get; set; }

        public string? SalesType { get; set; }

        public DateTime? SalesDate { get; set; }

        public int? TotalAmount { get; set; }

        public int? TotalGst { get; set; }

        public int? TotalIgst { get; set; }

        public int? TotalSgst { get; set; }

        public int SalesHeaderId { get; set; }

        public int SalesOrderDetailsId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Amount { get; set; }

        public int? Gst { get; set; }

        public int? Cgst { get; set; }

        public int? Igst { get; set; }
        public int? CategoryId { get; set; }
        public int? MRP { get; set; }


        public string CategoryName { get; set; }

        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

    }
}
