using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Model.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime SendDate { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }

    }
}
