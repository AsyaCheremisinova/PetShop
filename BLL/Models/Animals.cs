using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DAL.EF;

namespace BLL.Models
{ 
    public class Animals_Model
    {
       
        public int animal_id { get; set; }

        public string animal_name { get; set; }
        public Animals_Model() { }
        public Animals_Model(Animals a) 
        {
            animal_id = a.animal_id;
            animal_name = a.animal_name;
         //   Types = a.Types;
        }

        public ObservableCollection <Type_Model> Types { get; set; }
    }
}
