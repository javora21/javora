using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.View
{
    public class NewsModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
