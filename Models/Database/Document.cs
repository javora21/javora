using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.Database
{
    public enum DocumentType
    {
        UNDEFINED = 0
    }
    public class Document
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreateDate { get; set; }

        public string Extension { get; set; }

        public DocumentType FileType { get; set; }

    }
}
