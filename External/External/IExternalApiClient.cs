using System.Collections.Generic;
using Models;

namespace External
{
    public interface IExternalApiClient
    {
        List<ExternalApiAuthor> GetRandomAuthors();
    }
}