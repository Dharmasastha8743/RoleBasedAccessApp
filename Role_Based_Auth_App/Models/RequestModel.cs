namespace Role_Based_Auth_App.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public bool IsApproved { get; set; }
    }
}
