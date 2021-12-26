using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.ComponentModel;
using BLL.Models;
using System.Collections.ObjectModel;
using PetShop.Util;
using System.Windows.Input;
using BLL.Services;
using PetShop.View;


namespace PetShop.ViewModel
{
    class ShopVM : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private readonly IHistoryOrders historyOrder;
        private readonly IPrintCheck printCheck;

        //public string picture { get; set; }
        //public string product_name { get; set; }
        //public string description { get; set; }
        //public string availability { get; set; }
        //public decimal cost { get; set; }
        public ShopVM(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, IOrderService orderserv, IHistoryOrders historyOrders, IPrintCheck PrintCheck, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            orderService = orderserv;
            historyOrder = historyOrders;
            printCheck = PrintCheck;

            Catalog = new CatalogVM(crud, dialogService, typeProduct, userId);
            ShoppingBasket = new ShoppingBasketMV(crud, dialogService, typeProduct, orderService, userId);
            PersonalArea = new PersonalAreaVM(crud, dialogService, typeProduct, orderService, historyOrder, printCheck, userId);
            OrdersPage = new OrdersVM(crud, dialogService, typeProduct, orderService, historyOrders, userId);
            Home = new HomeVM(crud, dialogService, typeProduct, orderService, userId);
        }

        public CatalogVM Catalog { get; set; }
        public ShoppingBasketMV ShoppingBasket { get; set; }
        public PersonalAreaVM PersonalArea { get; set; }
        public OrdersVM OrdersPage { get; set; }
        public HomeVM Home { get; set; }

        //private ObservableCollection<Product_Model> Products;
        //public ObservableCollection<Product_Model> Product
        //{
        //    get
        //    {
        //        return Products;
        //    }
        //    set
        //    {
        //        Products = value;
        //        NotifyPropertyChanged("Product");
        //    }
        //}

        //private Product_Model _product;
        //public Product_Model product
        //{
        //    get
        //    {
        //        return _product;
        //    }
        //    set
        //    {
        //        _product = value;
        //        NotifyPropertyChanged("product");
        //    }
        //}




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
