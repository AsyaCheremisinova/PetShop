using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;


namespace BLL.Models
{
    public  class Pick_Up_Point_Model
    {
       
        public int pick_up_point_id { get; set; }

        public string pick_up_point_name { get; set; }
        public Pick_Up_Point_Model() { }
        public Pick_Up_Point_Model(Puck_Up_Point c)
        {
            pick_up_point_id = c.pick_up_point_id;
            pick_up_point_name = c.pick_up_point_name;
           
        }
    }
}
