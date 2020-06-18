using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.Database
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public List<Image> Images { get; set; }

        public Guid NewsGuid { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
