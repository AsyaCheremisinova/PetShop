using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DAL.EF
{
    public class TypeProd
    {
        public string animal_name { get; set; }
        public string type_name { get; set; }
    }

    public class CatalogProd
    {
        public int inventory_number { get; set; }
        public string product_name { get; set; }
        public decimal cost { get; set; }
        public string picture { get; set; }
    }
    public class Prod
    {
        public int inventory_number { get; set; }
        public string product_name { get; set; }
        public decimal cost { get; set; }
        public string picture { get; set; }
    }
    public class PopularProd
    {
        public int inventory_number { get; set; }
        public string product_name { get; set; }
        public decimal cost { get; set; }
        public string picture { get; set; }
        public int number { get; set; }
    }
    public class HistoryProduct
    {
        public int order_id { get; set; }
        public int order_line_id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public decimal cost { get; set; }
        public int number { get; set; }
    }

    public class CustomersOrders
    {
        public int order_id { get; set; }

        public decimal total_cost { get; set; }

        public DateTime date { get; set; }

        public int customer_id { get; set; }

        public int? status_id { get; set; }       
        
        public int Number { get; set; }
        public string status_name { get; set; }
        public int? pick_up_point_id { get; set; }
        public string point_name { get; set; }
    }
}
