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
    public class CatalogVM : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private int UserId;

        public CatalogVM(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            UserId = userId;

            var typeprod = typeProduct.GetAnimalsTypeModels();
            tree_Models = new ObservableCollection<Tree_Model>();

            foreach (var i in typeprod)
            {
                tree_Models.Add(i);
            }

            var tempProd = crud.GetAllProduct();
            Products = new ObservableCollection<Product_Model>();
            foreach (var i in tempProd)
            {
                Products.Add(i);
            }
            if (tempProd.Count == 0)
            {
                TextVisibility = "Visible";
            }
            else
            {
                TextVisibility = "Hidden";
            }
        }

        private string _picture;
        public string picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                NotifyPropertyChanged("picture");
            }
        }
        private string _product_name;
        public string product_name
        {
            get
            {
                return _product_name;
            }
            set
            {
                _product_name = value;
                NotifyPropertyChanged("product_name");
            }
        }
        private string _description;
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("description");
            }
        }
        private string _availability;
        public string Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                _availability = value;
                NotifyPropertyChanged("Availability");
            }
        }
        private decimal _cost;
        public decimal cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                NotifyPropertyChanged("cost");
            }
        }
        private int _number;
        public int number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("number");
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

        public ObservableCollection<Tree_Model> tree_Models { get; set; }

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

        private Product_Model _product;
        public Product_Model product {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                NotifyPropertyChanged("product");
            }
        }

        public Shopping_Basket_Model BasketProd { get; set; }

        //Выбрать конкретный товар
        private ICommand Index;
        public ICommand SelectedIndex
        {
            get
            {
                if (Index == null)
                    Index = new RelayCommand(args => SelectedIndexChanged(args));
                return Index;
            }
        }

        private void SelectedIndexChanged(object args)
        {
            Product_page(Product[(int)args].inventory_number);
        }

        private string browse;
        public string Browse {
            get
            {
                return browse;
            }
            set
            {
                browse = value;
                NotifyPropertyChanged("Browse");
            }
        }
        //поиск товара
        private ICommand find;
        public ICommand Find
        {
            get
            {
                if (find == null)
                    find = new RelayCommand(args => FindProduct(Browse));
                return find;
            }
        }

        private void FindProduct(object Browse)
        {
            Product.Clear();
            var tempProd = typeProduct.FindProduct((string)Browse);
            foreach (var i in tempProd)
            {
                Product.Add(i);
            }
            if(tempProd.Count == 0)
            {
                TextVisibility = "Visible";
            }
            else
            {
                TextVisibility = "Hidden";
            }
        }

        // Товары из дерева
        private string _selectedCategory;

        private ICommand _selectedItemChangedCommand;
        public ICommand SelectedItemChangedCommand
        {
            get
            {
                if (_selectedItemChangedCommand == null)
                    _selectedItemChangedCommand = new RelayCommand(args => SelectedItemChanged(args));
                return _selectedItemChangedCommand;
            }
        }

        private void SelectedItemChanged(object args)
        {
            _selectedCategory = args.ToString();

            Product.Clear();
            var tempProd = typeProduct.GetProductCatalod(_selectedCategory);
            foreach (var i in tempProd)
            {
                Product.Add(i);
            }

            TextVisibility = "Hidden";
        }

        //Отобразить выбранный товар
        public void Product_page(int id)
        {
            product = crud.GetProduct(id);
            picture = product.picture;
            product_name = product.product_name;
            description = product.description;
            cost = product.cost;
            if (product.availability == true && product.product_quantity > 0)
            {
                Visibility = "Visible";
                Availability = "Товар в наличии";
             }
            else
            {
                Visibility = "Hidden";
                Availability = "Товара нет в наличии";
            }
        }


        private List<Shopping_Basket_Model> b;
        //Добавление товара в корзину
        private ICommand Indexx;
        public ICommand AddProduct
        {
            get
            {
                if (Indexx == null)
                    Indexx = new RelayCommand(args => SelectedIndexxChanged(product));
                return Indexx;
            }
        }

        private void SelectedIndexxChanged(Product_Model args)
        {           
            product = args;
            //var temp = crud.GetAllBaskets().Count;
            b = new List<Shopping_Basket_Model>();
            b = crud.GetCustomersBaskets(UserId);
            int count = 0, id = 0;
            if (product.product_quantity > 0)
            {
                foreach (var i in b)
                {
                    if (product.inventory_number == i.inventory_number)
                    {
                        count++;
                        id = i.id_basket;
                    }
                }
                if (count == 0)
                {
                    Shopping_Basket_Model basket = new Shopping_Basket_Model()
                    {
                        inventory_number = product.inventory_number,
                        customer_id = UserId,
                        number = 1
                    };
                    crud.CreateProductBasket(basket);
                }
                else
                {
                    
                    var t = crud.GetBasket(id);
                    t.number = t.number + 1;
                    if (product.product_quantity >= t.number)
                        crud.AddBumberBaketProd(t);
                }
            }
           
            Messenger.Default.Send(new GenericMessage<Shopping_Basket_Model>(null));


        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
