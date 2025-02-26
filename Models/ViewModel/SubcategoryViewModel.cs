namespace ProductManagementSystem.Models.ViewModel
{
    public class SubcategoryViewModel
    {
        public int Id { get; set; }

        public string? SubName { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
