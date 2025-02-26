namespace ProductManagementSystem.Models
{
    public class LoginMaster
    {
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? CreateOn { get; set; }
    }
}
