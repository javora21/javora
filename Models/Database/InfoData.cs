using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.Database
{
    public class InfoData
    {
        public int Id { get; set; }

        public DateTime ChangeData { get; set; }

        public string HtmlData { get; set; }

        public InfoType InfoType { get; set; }
    }   
    public enum InfoType
    {
        RAILWAY_SCHEDULE = 1
    }
}
