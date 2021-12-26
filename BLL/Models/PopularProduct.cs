using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.ComponentModel;

namespace BLL.Models
{
    public class PopularProduct 
    {
        public int inventory_number { get; set; }
        public string product_name { get; set; }
        public decimal cost { get; set; }
        public string picture { get; set; }
        public int number { get; set; }
        public int count { get; set; }

    }
}
