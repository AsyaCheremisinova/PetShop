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
using System.Windows.Shapes;
using PetShop.ViewModel;
using BLL.Interfaces;
using PetShop.Util;

namespace PetShop
{
    /// <summary>
    /// Логика взаимодействия для Shop.xaml
    /// </summary>
    public partial class Shop : Window
    {
        public Shop(ICRUD crudService, IDialogService dialogService, ITypeProductService typeProduct, IOrderService orderService, IHistoryOrders historyOrders, IPrintCheck printCheck, int id)
        {
            InitializeComponent();
            DataContext = new ViewModel.ShopVM(crudService, dialogService, typeProduct, orderService, historyOrders, printCheck, id);
        }
    }
}
                         