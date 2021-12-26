using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.ComponentModel;
using BLL.Models;
using System.Collections.ObjectModel;

namespace PetShop.ViewModel
{
    class ProductVM
    {
        private readonly ICRUD crud;
        public string picture { get; set; }
        public string product_name { get; set; }
        public string description { get; set; }
        public string availability { get; set; }
        public decimal cost { get; set; }

        public ProductVM(int product_id ,ICRUD intCRUD)
        {
            crud = intCRUD;
            Product = crud.GetProduct(product_id);
            picture = Product.picture;
            product_name = Product.product_name;
            description = Product.description;
            cost = Product.cost;
            if(Product.availability == true)
            {
                availability = "Товар в наличии";
            }
            else
            {
                availability = "Товара нет в наличии";
            }
        }
        public Product_Model Product { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
