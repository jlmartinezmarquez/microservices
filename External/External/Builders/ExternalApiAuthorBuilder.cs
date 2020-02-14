using System.Collections.Generic;
using Models;

namespace External.Builders
{
    public class ExternalApiAuthorBuilder
    {
        private ExternalApiAuthor author;

        private ExternalApiAuthorBuilder()
        {
            this.author = new ExternalApiAuthor()
            {
            };
        }

        public static ExternalApiAuthorBuilder New() => new ExternalApiAuthorBuilder();

        public ExternalApiAuthorBuilder WithFirstName(string firstName)
        {
            this.author.FirstName = firstName;
            return this;
        }

        public ExternalApiAuthorBuilder WithLastName(string lastName)
        {
            this.author.LastName = lastName;
            return this;
        }

        public ExternalApiAuthorBuilder WithId(int id)
        {
            this.author.Id = id;
            return this;
        }

        public ExternalApiAuthorBuilder WithBooks(List<ExternalApiBook> books)
        {
            this.author.Books = books;
            return this;
        }

        public ExternalApiAuthor Build() => this.author;
    }
}
