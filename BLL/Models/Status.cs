using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace BLL.Models
{ 

    public  class Status_Model
    {
        
        public int status_id { get; set; }

        public string status_name { get; set; }

        public Status_Model() { }
        public Status_Model(Status c)
        {
            status_id = c.status_id;
            status_name = c.status_name;

        }
    }
}
