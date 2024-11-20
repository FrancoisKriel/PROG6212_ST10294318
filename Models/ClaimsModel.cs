namespace CMCS.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Pending"; // Default status
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}