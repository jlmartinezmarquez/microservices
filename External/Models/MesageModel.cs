using System.Collections.Generic;

namespace Models
{
    public class MesageModel
    {
        public Microservice Receiver { get; set; }
        public ReadOrWrite Operation { get; set; }
        public List<ExternalApiAuthor> Author { get; set; }
        public List<ExternalApiBook> Book { get; set; }
    }
}
