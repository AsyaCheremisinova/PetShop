using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using BLL.Interfaces;
using BLL.Util;
using PetShop.Util;
using Ninject;

              
namespace PetShop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel(new NinjectReg(), new ServiceModule());

            ICRUD crudServ = kernel.Get<ICRUD>();
            IDialogService dialogService =new DialogService();
            ITypeProductService typeProduct = kernel.Get<ITypeProductService>();
            IOrderService orderService = kernel.Get<IOrderService>();
            IHistoryOrders historyOrders = kernel.Get<IHistoryOrders>();
            IAuthorizationService authorizationService = kernel.Get<IAuthorizationService>();
            IPrintCheck printCheck = kernel.Get<IPrintCheck>();


            MainWindow mainWindow = new MainWindow(crudServ, dialogService, typeProduct, orderService, historyOrders, authorizationService, printCheck);

            mainWindow.Show();
        }
    }
}
