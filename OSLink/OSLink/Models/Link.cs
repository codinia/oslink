using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSLink.Models
{
    [Table("Users")]
    public class Link
    {
        public int ID { get; set; }
        public string UID { get; set; }
        public string Application_Name { get; set; }
        public string AppStore_url { get; set; }
        public string Ipad { get; set; }
        public string Android_url { get; set; }
        public string Huawei_url { get; set; }
        public string Microsoft_store { get; set; }
        public string BlackBerry_url { get; set; }
        public string Kindel_url { get; set; }
        public string Anyother_url { get; set; }
    }
}
