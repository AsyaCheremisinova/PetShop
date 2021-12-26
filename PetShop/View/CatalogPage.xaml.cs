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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : UserControl
    {
        public CatalogPage()
        {
            InitializeComponent();
        }

        private void product_click(object sender, MouseButtonEventArgs e)
        {
           // if(productsList.SelectedIndex != -1)
            this.productGrid.Visibility = Visibility;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
           // productsList.SelectedIndex = -1;
            this.productGrid.Visibility = Visibility.Hidden;
        }

       
    }
}
