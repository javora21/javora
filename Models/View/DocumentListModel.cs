using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.View
{
    public class DocumentListModel
    {
        public List<DocumentModel> Docs { get; set; }

        public PaginationModel Pagination { get; set; }
    }
}
