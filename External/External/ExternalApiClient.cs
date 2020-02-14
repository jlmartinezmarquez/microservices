using System;
using System.Collections.Generic;
using External.Builders;
using Models;

namespace External
{
    public class ExternalApiClient : IExternalApiClient
    {
        private List<ExternalApiAuthor> _authors;

        public List<ExternalApiAuthor> GetRandomAuthors()
        {
            _authors = new List<ExternalApiAuthor>()
            {
                ExternalApiAuthorBuilder.New().WithFirstName("George").WithLastName("Pelecanos").WithId(1)
                    .WithBooks(
                        new List<ExternalApiBook>()
                        {
                            ExternalApiBookBuilder.New().WithAuthorID(1).WithIsbn("1586216007").WithTitle("Hard revolution").Build(),
                            ExternalApiBookBuilder.New().WithAuthorID(1).WithIsbn("9780575071704").WithTitle("Right as rain").Build(),
                        })
                    .Build(),
                ExternalApiAuthorBuilder.New().WithFirstName("John").WithLastName("Steinbeck").WithId(2)
                    .WithBooks(
                        new List<ExternalApiBook>()
                        {
                            ExternalApiBookBuilder.New().WithAuthorID(2).WithIsbn("9780230031050").WithTitle("The grapes of wrath").Build(),
                        })
                    .Build(),
                ExternalApiAuthorBuilder.New().WithFirstName("Irvine").WithLastName("Welsh").WithId(3)
                    .WithBooks(
                        new List<ExternalApiBook>()
                        {
                            ExternalApiBookBuilder.New().WithAuthorID(3).WithIsbn("9788379980147").WithTitle("Trainspotting").Build(),
                            ExternalApiBookBuilder.New().WithAuthorID(3).WithIsbn("9780393088731").WithTitle("Skagboys").Build()
                        })
                    .Build()
            };

            return _authors;
        }
    }
}
