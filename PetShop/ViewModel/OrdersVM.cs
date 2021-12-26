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
using GalaSoft.MvvmLight.Messaging;
using PetShop.View;


namespace PetShop.ViewModel
{
    public class OrdersVM : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private readonly IHistoryOrders historyOrders;
        private int UserId;
        public OrdersVM(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, IOrderService orderserv, IHistoryOrders historyOrder, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            orderService = orderserv;
            historyOrders = historyOrder;
            UserId = userId;

            customer = crud.GetCustomer(UserId);
            var temp = crud.CustomersOrders(UserId);
            orders = new ObservableCollection<Order_Model>();
            foreach(var i in temp)
            {   
                orders.Add(i);
                i.historyProducts = new ObservableCollection<HistoryProduct_Model>();
                var histProd = historyOrders.GetHistory(i.order_id).ToList();
                foreach (var j in histProd)
                {
                    i.historyProducts.Add(j);
                }
            }
            if(orders.Count == 0)
            { 
                Visibility = "Visible";
            }
            else Visibility = "Hidden";
            Messenger.Default.Register<GenericMessage<Order_Model>>(this, UpdateOrders);

        }
        public Customer_Model customer { get; set; }
        public ObservableCollection<Order_Model> orders { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private string visibility;
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                NotifyPropertyChanged("Visibility");
            }
        }


        // Обновление списка доставок
        private void UpdateOrders(GenericMessage<Order_Model> msg)
        {
            orders.Clear();
            var temp = crud.CustomersOrders(UserId);
            foreach (var i in temp)
            {
                orders.Add(i);
                i.historyProducts = new ObservableCollection<HistoryProduct_Model>();
                var histProd = historyOrders.GetHistory(i.order_id).ToList();
                foreach (var j in histProd)
                {
                    i.historyProducts.Add(j);
                }
            }
            if (orders.Count == 0)
            {
                Visibility = "Visible";
            }
            else Visibility = "Hidden";
            

        }


        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
