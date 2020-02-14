using System;
using System.Collections.Generic;
using External.Builders;
using Models;

namespace External
{
    public class ExternalApiClient : IExternalApiClient
    {
        private List<ExternalApiAuthor> _authors;

        private List<ExternalApiBook> _books;

        public List<ExternalApiAuthor> GetRandomAuthors()
        {
            _authors = new List<ExternalApiAuthor>()
            {
                ExternalApiAuthorBuilder.New().WithFirstName("George").WithLastName("Pelecanos").WithId(1).Build(),
                ExternalApiAuthorBuilder.New().WithFirstName("John").WithLastName("Steinbeck").WithId(2).Build(),
                ExternalApiAuthorBuilder.New().WithFirstName("Irvine").WithLastName("Welsh").WithId(3).Build()
            };

            //var randomNumber = new Random().Next(1, _authors.Count);

            //var toReturn = new List<ExternalApiAuthor>();
            //for (var i = 0; i < randomNumber; i++)
            //{
            //    var anotherRandomNumber = new Random().Next(1, _authors.Count);
            //    toReturn.Add(_authors[anotherRandomNumber]);
            //}

            return _authors;
        }

        public List<ExternalApiBook> GetRandomBooks()
        {
            _books = new List<ExternalApiBook>()
            {
                ExternalApiBookBuilder.New().WithAuthorID(1).WithIsbn("1586216007").WithTitle("Hard revolution").Build(),
                ExternalApiBookBuilder.New().WithAuthorID(2).WithIsbn("9780230031050").WithTitle("The grapes of wrath").Build(),
                ExternalApiBookBuilder.New().WithAuthorID(1).WithIsbn("9780575071704").WithTitle("Right as rain").Build(),
                ExternalApiBookBuilder.New().WithAuthorID(3).WithIsbn("9788379980147").WithTitle("Trainspotting").Build(),
                ExternalApiBookBuilder.New().WithAuthorID(3).WithIsbn("9780393088731").WithTitle("Skagboys").Build()
            };

            //var randomNumber = new Random().Next(1, _books.Count);

            //var toReturn = new List<ExternalApiBook>();
            //for (var i = 0; i < randomNumber; i++)
            //{
            //    var anotherRandomNumber = new Random().Next(1, _books.Count);
            //    toReturn.Add(_books[anotherRandomNumber]);
            //}

            return _books;
        }
    }
}
