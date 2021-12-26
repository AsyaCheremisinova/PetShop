using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace BLL.Models
{
    public  class Order_line_Model
    {
       
        public int order_line_id { get; set; }

        public decimal order_line_cost { get; set; }

        public int inventory_number { get; set; }

        public int order_id { get; set; }
        public int number { get; set; }
        public Order_line_Model() { }

        public Order_line_Model(Order_line o_line)
        {
            order_line_id = o_line.order_line_id;
            order_line_cost = o_line.order_line_cost;
            inventory_number = o_line.inventory_number;
            order_id = o_line.order_id;
            number = o_line.number;
        }
    }
}
