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

namespace PetShop.View
{
    /// <summary>
    /// Логика взаимодействия для ShoppingBasketPage.xaml
    /// </summary>
    public partial class ShoppingBasketPage : UserControl
    {
        public ShoppingBasketPage()
        {
            InitializeComponent();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            this.OrderGrid.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.OrderGrid.Visibility = Visibility.Hidden;
        }
    }
}
