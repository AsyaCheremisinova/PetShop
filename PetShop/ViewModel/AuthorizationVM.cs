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

namespace PetShop.ViewModel
{
    public class AuthorizationVM : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private readonly IHistoryOrders historyOrders;
        private readonly IAuthorizationService authorizationService;
        private readonly IPrintCheck printCheck;
        public delegate void DialogHandler();
        public event DialogHandler Notify;

        public AuthorizationVM(ICRUD crudService, IDialogService dialogServ, ITypeProductService type, IOrderService orderServ, IHistoryOrders historyOrd, IAuthorizationService authorization, IPrintCheck PrintCheck)
        {
            crud = crudService;
            dialogService = dialogServ;
            typeProduct = type;
            orderService = orderServ;
            historyOrders = historyOrd;
            authorizationService = authorization;
            printCheck = PrintCheck;
            Visibility = "Hidden";
            Login = "";
            Password = "";
            try
            {
                authorization.CheckLogin(Login);
            }
            catch
            {
                Visibility = "Visible";
            }
        }



        private ICommand сheck;
        public ICommand CheckCustomer
        {
            get
            {
                if (сheck == null)
                    сheck = new RelayCommand(args => Check(args));
                return сheck;
            }
        }
        private void Check(object args)
        {
            if (authorizationService.CheckPassword(Login, Password) == false)
            {
                return;
            }
            int UserId = authorizationService.GetUser(Login);
            dialogService.OpenShop(crud, dialogService, typeProduct, orderService, historyOrders, printCheck, UserId);
            Notify?.Invoke();
        }


        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                    close = new RelayCommand(args => _Close());
                return close;
            }
        }
        private void _Close()
        {
            Visibility = "Hidden";
        }
        

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                NotifyPropertyChanged("Login");
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
