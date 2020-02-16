using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.Database
{
    public class Image
    {
        public int Id { get; set; }

        public string Puth { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public News News { get; set; }

    }
}
