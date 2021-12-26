using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class Tree_Model
    {
        public string animal_name { get; set; }
        public ObservableCollection <Product_Type_Model> type_name { get; set; }
    }
}
