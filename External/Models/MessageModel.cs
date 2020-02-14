using System.Collections.Generic;

namespace Models
{
    public class MessageModel
    {
        public Subscriptor Receiver { get; set; }
        public ReadOrWrite Operation { get; set; }
        public ExternalApiAuthor Author { get; set; }
        public List<ExternalApiBook> Books { get; set; }
    }
}
