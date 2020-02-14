using Models;

namespace External.Builders
{
    public class ExternalApiBookBuilder
    {
        private ExternalApiBook book;

        private ExternalApiBookBuilder()
        {
            this.book = new ExternalApiBook()
            {
            };
        }

        public static ExternalApiBookBuilder New() => new ExternalApiBookBuilder();

        public ExternalApiBookBuilder WithIsbn(string isbn)
        {
            this.book.Isbn = isbn;
            return this;
        }

        public ExternalApiBookBuilder WithTitle(string title)
        {
            this.book.Title = title;
            return this;
        }

        public ExternalApiBookBuilder WithAuthorID(int authorId)
        {
            this.book.AuthorId = authorId;
            return this;
        }

        public ExternalApiBook Build() => this.book;
    }
}
