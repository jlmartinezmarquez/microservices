namespace Writer.Models
{
    public class Book
    {
        public string Isbn { get; set; }

        public string Title { get; set; }
        
        public int YearPublished { get; set; }

        public string Publisher { get; set; }

        public int AuthorId { get; set; }
        
        public int AuthorFirstName { get; set; }

        public int AuthorLastName { get; set; }
    }
}
