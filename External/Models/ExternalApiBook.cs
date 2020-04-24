namespace Models
{
    public class ExternalApiBook
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public int YearPublished { get; set; }

        public string Publisher { get; set; }

        public int AuthorId { get; set; }
    }
}
