namespace ExpenseTrackAPI.Models
{
    public class Category : TableBase
    {
        public int Id    { get; set; }
        public string Description { get; set; }
    }
}
