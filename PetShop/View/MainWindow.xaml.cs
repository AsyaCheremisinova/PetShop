using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PetShop.ViewModel;
using BLL.Interfaces;
using PetShop.Util;

namespace PetShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICRUD crudServ;
        public IDialogService dialogService;
        public ITypeProductService typeProduct;
        public IOrderService orderService;
        public IHistoryOrders historyOrders;
        public IAuthorizationService authorizationService;
        

        public MainWindow(ICRUD crudService, IDialogService dialogService, ITypeProductService typeProduct, IOrderService orderService, IHistoryOrders historyOrders, IAuthorizationService authorization, IPrintCheck printCheck)
        {
            InitializeComponent();

            AuthorizationVM vm = new AuthorizationVM(crudService, dialogService, typeProduct, orderService, historyOrders, authorization, printCheck);
            DataContext = vm;
        }


        //private void EnterClientApp_Click(object sender, RoutedEventArgs e)
        //{
        //    Shop shopWindow = new Shop(crudServ, dialogService, typeProduct, orderService, historyOrders);
        //    shopWindow.Show();
        //    this.Close();
        //}

    }
}
