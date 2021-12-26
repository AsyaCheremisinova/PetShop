using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace BLL.Models
{
    public partial class Product_Type_Model
    {
        
        public int type_id { get; set; }

       
        public string type_name { get; set; }


        public Product_Type_Model() { }

        public Product_Type_Model(Product_Type pr)
        {
            type_id = pr.type_id;
            type_name = pr.type_name;
        }

    }
}
