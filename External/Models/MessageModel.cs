using System.Collections.Generic;

namespace Models
{
    public class MessageModel
    {
        public ReadOrWrite Operation { get; set; }
        public ExternalApiAuthor Author { get; set; }
        public List<ExternalApiBook> Books { get; set; }
    }
}
