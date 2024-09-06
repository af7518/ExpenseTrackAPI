namespace ExpenseTrackAPI.Models
{
    public class Expense : TableBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DueDate { get; set; }
        public int Order { get; set; }
    }
}
