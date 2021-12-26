using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Type = DAL.EF.Type;

namespace BLL.Models
{
    public partial class Type_Model
    {
        public int id_type { get; set; }

        public int type_id { get; set; }

        public int animal_id { get; set; }
        public Type_Model() { }
        public Type_Model(Type type ) 
        {
            id_type = type.type_id;
            type_id = type.type_id;
            animal_id = type.animal_id;
        }
    }
}
