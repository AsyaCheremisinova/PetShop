using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class HistoryProduct_Model
    {
        public int order_id { get; set; }
        public int order_line_id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public decimal cost { get; set; }
        public int number { get; set; }
    }
}
