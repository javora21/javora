using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.View
{
    public class PaginationModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
    }
}
