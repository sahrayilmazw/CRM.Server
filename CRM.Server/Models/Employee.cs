namespace CRM.Server.Models
{
    public class Employee
    {
        public int Id { get; set; } //uint negatif değer alamaz
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

    }
}
