using System.Collections.Generic;

namespace Models
{
    public class ExternalApiAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ExternalApiBook> Books { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
