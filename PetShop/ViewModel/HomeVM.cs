using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.ComponentModel;
using BLL.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using PetShop.Util;
using System.Windows.Input;
using BLL.Services;
using PetShop.View;

namespace PetShop.ViewModel
{
    public class HomeVM : INotifyPropertyChanged
    {


        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private int UserId;

        public HomeVM(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, IOrderService orderserv, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            orderService = orderserv;
            UserId = userId;

            var tempProd = typeProduct.PopularProducts();

            Product = new ObservableCollection<Product_Model>();
            int count = 0;
            foreach (var j in tempProd)
            {
                if (count < 10)
                {
                    Product.Add(j);
                    count++;
                }
            }
            Messenger.Default.Register<GenericMessage<Order_Model>>(this, Update);

        }



        private ObservableCollection<Product_Model> Products;
        public ObservableCollection<Product_Model> Product
        {
            get
            {
                return Products;
            }
            set
            {
                Products = value;
                NotifyPropertyChanged("Product");
            }
        }


        private void Update(GenericMessage<Order_Model> msg)
        {
            Product.Clear();
            var tempProd = typeProduct.PopularProducts();
            int count = 0;
            foreach (var j in tempProd)
            {
                if (count < 10)
                {
                    Product.Add(j);
                    count++;
                }
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
