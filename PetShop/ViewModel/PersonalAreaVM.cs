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
    public class PersonalAreaVM : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private readonly IHistoryOrders historyOrders;
        private readonly IPrintCheck printCheck;
        private int UserId;
        public PersonalAreaVM(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, IOrderService orderserv, IHistoryOrders historyOrder, IPrintCheck PrintCheck, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            orderService = orderserv;
            historyOrders = historyOrder;
            UserId = userId;
            printCheck = PrintCheck;

            customer = crud.GetCustomer(UserId);
            name = customer.customer_name;
            var orders = crud.GetCustomersOrder(UserId);
            decimal tempcost = 0;
            foreach (var i in orders)
            {
                tempcost += i.total_cost;

            }
            if (orders.Count == 0)
            {
                TextVisibility = "Visible";
                CostsTextVisibility = "Visible";
            }
            else
            {
                TextVisibility = "Hidden";
                CostsTextVisibility = "Hidden";
            }
            Visibility = "Hidden";
            CostsVisibility = "Hidden";
            if (tempcost >= 50000)
            {
                customer.sale = 10;
                crud.ChangeSale(customer);
            }
            else
            {
                if (tempcost >= 30000)
                {
                    customer.sale = 7;
                    crud.ChangeSale(customer);
                }
                else
                {
                    if (tempcost >= 20000)
                    {
                        customer.sale = 5;
                        crud.ChangeSale(customer);
                    }
                    else
                    {
                        customer.sale = 3;
                        crud.ChangeSale(customer);
                    }
                }
            }

            switch (customer.sale)
            {
                case 3: 
                    {
                        Color1 = "#FFD95A51";
                        Color2 = "#FFB9B6B6";
                        Color3 = "#FFB9B6B6";
                        Color4 = "#FFB9B6B6";
                    } break;
                case 5:
                    {
                        Color1 = "#FFB9B6B6";
                        Color2 = "#FFD95A51";
                        Color3 = "#FFB9B6B6";
                        Color4 = "#FFB9B6B6";
                    }break;
                case 7:
                    {
                        Color1 = "#FFB9B6B6";
                        Color2 = "#FFB9B6B6";
                        Color3 = "#FFD95A51";
                        Color4 = "#FFB9B6B6";
                    }
                    break;
                case 10:
                    {
                        Color1 = "#FFB9B6B6";
                        Color2 = "#FFB9B6B6";
                        Color3 = "#FFB9B6B6";
                        Color4 = "#FFD95A51";
                    }
                    break;
            }
            CustomerCost = $"Ваша сумма выкупа: {tempcost.ToString()} руб.";

            Orders = new ObservableCollection<Order_Model>();
            foreach (var i in orders)
            {
                i.historyProducts = new ObservableCollection<HistoryProduct_Model>();   
                
                   Orders.Add(i);
                    var histProd = historyOrders.GetHistory(i.order_id).ToList();
                    foreach (var j in histProd)
                    {
                        i.historyProducts.Add(j);
                        i.Number += j.number;                       
                    }               
            }
            Costs = new ObservableCollection<Order_Model>();
            foreach (var i in orders)
            {               
                Costs.Add(i);                
            }

            var date1String = "1/01/2021";
            Date1 = DateTime.Parse(date1String,System.Globalization.CultureInfo.InvariantCulture);            
            Date2 = DateTime.Now;
            CostsDate1 = DateTime.Parse(date1String, System.Globalization.CultureInfo.InvariantCulture);
            CostsDate2 = DateTime.Now;
        }

        public string name { get; set; }

        public Customer_Model customer { get; set; }
        public string CustomerCost { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public ObservableCollection<Order_Model> Orders { get; set; }
        public ObservableCollection<Order_Model> Costs { get; set; }

        private DateTime date1;
        public DateTime Date1
        {
            get
            {
                return date1;
            }
            set
            {
                date1 = value;
                NotifyPropertyChanged("Date1");
            }
        }
        private DateTime date2;
        public DateTime Date2
        {
            get
            {
                return date2;
            }
            set
            {
                date2 = value;
                NotifyPropertyChanged("Date2");
            }
        }
        private DateTime Cdate1;
        public DateTime CostsDate1
        {
            get
            {
                return Cdate1;
            }
            set
            {
                Cdate1 = value;
                NotifyPropertyChanged("CostsDate1");
            }
        }
        private DateTime Cdate2;
        public DateTime CostsDate2
        {
            get
            {
                return Cdate2;
            }
            set
            {
                Cdate2 = value;
                NotifyPropertyChanged("CostsDate2");
            }
        }
        private string textVisibility;
        public string TextVisibility
        {
            get
            {
                return textVisibility;
            }
            set
            {
                textVisibility = value;
                NotifyPropertyChanged("TextVisibility");
            }
        }
        private string coststextVisibility;
        public string CostsTextVisibility
        {
            get
            {
                return coststextVisibility;
            }
            set
            {
                coststextVisibility = value;
                NotifyPropertyChanged("CostsTextVisibility");
            }
        }
        
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
        private string costsvisibility;

        public string CostsVisibility
        {
            get
            {
                return costsvisibility;
            }
            set
            {
                costsvisibility = value;
                NotifyPropertyChanged("CostsVisibility");
            }
        }
        
        public ObservableCollection<int> Numbers { get; set; }

        private ICommand date;
        public ICommand SearchDate
        {
            get
            {
                if (date == null)
                    date = new RelayCommand(args => FindDate(Date1, Date2));
                return date;
            }
        }

        private void FindDate(DateTime Date1, DateTime Date2)
        {
            Orders.Clear();

            var orders = crud.GetCustomersOrder(UserId);
            var periodOrd = crud.GetPeriodOrders(orders, Date1, Date2);
            if (periodOrd.Count == 0)
            {
                TextVisibility = "Visible";
            }
            else TextVisibility = "Hidden";
            foreach (var i in periodOrd)
            {
                i.historyProducts = new ObservableCollection<HistoryProduct_Model>();               
                    Orders.Add(i);
                    var histProd = historyOrders.GetHistory(i.order_id).ToList();
                    foreach (var j in histProd)
                    {
                        i.historyProducts.Add(j);
                        i.Number += j.number;
                    }                
            }
        }
        private ICommand Costdate;
        public ICommand SearchDateCosts
        {
            get
            {
                if (Costdate == null)
                    Costdate = new RelayCommand(args => SearchDateCost(CostsDate1, CostsDate2));
                return Costdate;
            }
        }
        private void SearchDateCost(DateTime CostsDate1, DateTime CostsDate2)
        {
            Costs.Clear();

            var orders = crud.GetCustomersOrder(UserId);
            var periodOrd = crud.GetPeriodOrders(orders, CostsDate1, CostsDate2);
            if (periodOrd.Count == 0)
            {
                CostsTextVisibility = "Visible";
            }
            else CostsTextVisibility = "Hidden";
            foreach (var i in periodOrd)
            {
                i.historyProducts = new ObservableCollection<HistoryProduct_Model>();
                Costs.Add(i);                
            }
        }

        private ICommand open;
        public ICommand OpenHistory
        {
            get
            {
                if (open == null)
                    open = new RelayCommand(args => OpenHist());
                return open;
            }
        }
        private void OpenHist()
        {
            Visibility = "Visible";
        }
        private ICommand close;
        public ICommand CloseHistory
        {
            get
            {
                if (close == null)
                    close = new RelayCommand(args => CloseHist());
                return close;
            }
        }
        private void CloseHist()
        {
            Visibility = "Hidden";
        }


        private ICommand openC;
        public ICommand OpenCosts
        {
            get
            {
                if (openC == null)
                    openC = new RelayCommand(args => OpenCostsForm());
                return openC;
            }
        }
        private void OpenCostsForm()
        {
            CostsVisibility = "Visible";
        }
        private ICommand closeC;
        public ICommand CloseCosts
        {
            get
            {
                if (closeC == null)
                    closeC = new RelayCommand(args => CloseCostsForm());
                return closeC;
            }
        }
        private void CloseCostsForm()
        {
            CostsVisibility = "Hidden";
        }
        

        private ICommand check;
        public ICommand GetCheck
        {
            get
            {
                if (check == null)
                    check = new RelayCommand(args => GetOrderCheck(args));
                return check;
            }
        }
        private void GetOrderCheck(object args)
        {
            printCheck.GetCheck(Orders[(int)args]);
        }
        private ICommand rep;
        public ICommand GetReport
        {
            get
            {
                if (rep == null)
                    rep = new RelayCommand(args => GetCustomerReport());
                return rep;
            }
        }
        private void GetCustomerReport()
        {
            printCheck.GetReport(Costs, UserId, CostsDate1, CostsDate2);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
