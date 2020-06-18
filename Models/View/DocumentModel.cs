using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.View
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
