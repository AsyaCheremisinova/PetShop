using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BLL.Models
{
    public class Shopping_Basket_Model : INotifyPropertyChanged
    {
        public int id_basket
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged("id_basket");
            }
        }
        private int id;
        public int inventory_number { get; set; }
        public int customer_id { get; set; }
        public int number { get; set; }

        public Shopping_Basket_Model() { }
        public Shopping_Basket_Model(Shopping_Basket s)
        {
            id_basket = s.id_basket;
            inventory_number = s.inventory_number;
            customer_id = s.customer_id;
            number = s.number;
        }

        public ObservableCollection<Product_Model> products_from_basket { get; set; }

       public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
