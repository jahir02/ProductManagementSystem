namespace ProductManagementSystem.Models.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ModelNo { get; set; }

        public string? Specification { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}
