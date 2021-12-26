using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace BLL.Models
{ 
    public class Customer_Model
    {
        
        public int customer_id { get; set; }

        public string customer_name { get; set; }

        public string customer_login { get; set; }

        public string customer_password { get; set; }
        public int sale { get; set; }
        public Customer_Model() { }
        public Customer_Model(Customer c)
        {
            customer_id = c.customer_id;
            customer_name = c.customer_name;
            customer_login = c.customer_login;
            customer_password = c.customer_password;
            sale = c.sale;
        }


    }
}
