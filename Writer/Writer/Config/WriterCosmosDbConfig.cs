namespace Writer.Config
{
    public class WriterCosmosDbConfig
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string PrimaryPassword { get; set; }

        public string ConnectionString { get; set; }

        public string DataBaseId { get; set; }

        public string AuthorsCollectionId { get; set; }
        
        public string BooksCollectionId { get; set; }
    }
}
