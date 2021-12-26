using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.ComponentModel;

namespace BLL.Models
{
    public partial class Product_Model : INotifyPropertyChanged
    {
        
        public int inventory_number { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public bool availability { get; set; }

        public decimal cost { get; set; }

        public decimal? weight { get; set; }

        public int type_id { get; set; }

        public int animal_id { get; set; }

        public int product_quantity { get; set; }
        public string picture { get; set; }
        public int? number { get; set; }
        public int count { get; set; }

        public Product_Model() { }
        public Product_Model(Product p) 
        {
            inventory_number = p.inventory_number;
            product_name = p.product_name;
            description = p.description;
            availability = p.availability;
            cost = p.cost;
            weight = p.weight;
            type_id = p.type_id;
            animal_id = p.animal_id;
            product_quantity = p.product_quantity;
            picture = p.picture;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
