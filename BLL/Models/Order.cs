using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.Collections.ObjectModel;


namespace BLL.Models
{
    public  class Order_Model
    {
       
       
        public int order_id { get; set; }

        public decimal total_cost { get; set; }

        public DateTime date { get; set; }

        public int customer_id { get; set; }

        public int? pick_up_point_id { get; set; }

        public int? status_id { get; set; }
        public int IDprod { get; set; }
        public string Order_line { get; set; }
        public List<int> Order_lineId { get; set; }
        public Order_Model() { }
        public Order_Model(Order a)
        {
            order_id = a.order_id;
            total_cost = a.total_cost;
            date = a.date;
            customer_id = a.customer_id;
            pick_up_point_id = a.pick_up_point_id;
            status_id = a.status_id;
            //Order_line = String.Join(",", a.Order_lines.Select(i => i.inventory_number).ToList());
            //Order_lineId = a.Order_lines.Select(i => i.order_line_id).ToList();
        }
        public ObservableCollection<HistoryProduct_Model> historyProducts { get; set; }
        public int Number { get; set; }
        public string status_name { get; set; }
        public string point_name { get; set; }
    }
}
