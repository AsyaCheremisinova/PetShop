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
    public class ShoppingBasketMV : INotifyPropertyChanged
    {
        private readonly ICRUD crud;
        private readonly IDialogService dialogService;
        private readonly ITypeProductService typeProduct;
        private readonly IOrderService orderService;
        private int UserId;

        public ShoppingBasketMV(ICRUD intCRUD, IDialogService DialogService, ITypeProductService type, IOrderService orderserv, int userId)
        {
            crud = intCRUD;
            dialogService = DialogService;
            typeProduct = type;
            orderService = orderserv;
            UserId = userId;

            products = new ObservableCollection<Product_Model>();
        
            Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            costOrder = 0;
            int count = 0;
            foreach (var i in Shopping_Basket_Product)
            {               
                    var product = crud.GetProduct(i.inventory_number);
                    product.number = i.number;
                    if (product.number == 0) count++;
                    product.cost = product.cost * i.number;
                    products.Add(product);
                    costOrder = costOrder + product.cost;                
            }
           
            //скидка
            customer = new Customer_Model();
            customer = crud.GetCustomer(UserId);
            sale = costOrder * customer.sale / 100;

            OrderCost = costOrder - sale;
            if (products.Count == 0)
            {
                Visibility = "Visible";
                ButtonVisibility = "Hidden";
            }
            else
            {
                Visibility = "Hidden";
                if (count != 0)
                    ButtonVisibility = "Hidden";
                else
                    ButtonVisibility = "Visible";
            }
            OrderFormVisibility = "Hidden";
            EmptyProducts = new ObservableCollection<Product_Model>();
           
             EmptyVisibility = "Hidden";
             TextVisibility = "Hidden";
             var typepoint = crud.GetAllPoint();
            
            points = new ObservableCollection<Pick_Up_Point_Model>();
           
            foreach (var i in typepoint)
            {
                points.Add(i);
            }
            Messenger.Default.Register<GenericMessage<Shopping_Basket_Model>>(this, UpdateBasket);
        }
        private string emptyvisibility;
        public string EmptyVisibility
        {
            get
            {
                return emptyvisibility;
            }
            set
            {
                emptyvisibility = value;
                NotifyPropertyChanged("EmptyVisibility");
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
        private string buttonVisibility;
        public string ButtonVisibility
        {
            get
            {
                return buttonVisibility;
            }
            set
            {
                buttonVisibility = value;
                NotifyPropertyChanged("ButtonVisibility");
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
        private string orderFormVisibility;
        public string OrderFormVisibility
        {
            get
            {
                return orderFormVisibility;
            }
            set
            {
                orderFormVisibility = value;
                NotifyPropertyChanged("OrderFormVisibility");
            }
        }
        private List<Shopping_Basket_Model> Shopping_Basket_Product { get; set; }
        //public ObservableCollection<Shopping_Basket_Model> shopping_Basket { get; set; }
        private ObservableCollection<Product_Model> Products;
        public ObservableCollection<Product_Model> products /*{ get; set; }*/
        {
            get
            {
                return Products;
            }
            set
            {
                Products = value;
                NotifyPropertyChanged("products");
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

        public ObservableCollection<Pick_Up_Point_Model> points { get; set; }
        private Pick_Up_Point_Model point;
        public Pick_Up_Point_Model Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
                NotifyPropertyChanged("Point");
            }
        }
        private decimal costOrd;
        public decimal costOrder
        {
            get
            {
                return costOrd;
            }
            set
            {
                costOrd = value;
                NotifyPropertyChanged("costOrder");
            }
        }
        private decimal Sale;
        public decimal sale
        {
            get
            {
                return Sale;
            }
            set
            {
                Sale = value;
                NotifyPropertyChanged("sale");
            }
        }
        private decimal ordercost;
        public decimal OrderCost
        {
            get
            {
                return ordercost;
            }
            set
            {
                ordercost = value;
                NotifyPropertyChanged("OrderCost");
            }
        }
        public ObservableCollection<Product_Model> EmptyProducts { get; set; }
        public Customer_Model customer { get; set; }

        public Order_Model order { get; set; }


        private ICommand id;
        public ICommand DeleteProduct
        {
            get
            {
                if (id == null)
                    id = new RelayCommand(args => Delete(args));
                return id;
            }
        }

        //Удаление товара из корзины
        private void Delete(object args)
        {
            crud.DeleteBasketProduct(Shopping_Basket_Product[(int)args].id_basket);
            Shopping_Basket_Product.Clear();
            products.Clear();
            Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            costOrder = 0;
            int count = 0;
            foreach (var i in Shopping_Basket_Product)
            {
                var product = crud.GetProduct(i.inventory_number);
                product.number = i.number;
                if (product.number == 0) count++;
                product.cost = product.cost * i.number;
                products.Add(product);
                costOrder = costOrder + product.cost;
            }

            //скидка
            customer = new Customer_Model();
            customer = crud.GetCustomer(UserId);
            sale = costOrder * customer.sale / 100;

            OrderCost = costOrder - sale;
            if (products.Count == 0)
            {
                Visibility = "Visible";
                ButtonVisibility = "Hidden";
            }
            else
            {
                Visibility = "Hidden";
                if (count != 0)
                    ButtonVisibility = "Hidden";
                else
                    ButtonVisibility = "Visible";
            }
            

        }

        private ICommand openOrd;
        public ICommand OpenOrderForm
        {
            get
            {
                if (openOrd == null)
                    openOrd = new RelayCommand(args => OpenOrder());
                return openOrd;
            }
        }
        private void OpenOrder()
        {
            OrderFormVisibility = "Visible";
        }

        private ICommand closeOrd;
        public ICommand CloseOrderForm
        {
            get
            {
                if (closeOrd == null)
                    closeOrd = new RelayCommand(args => CloseOrder());
                return closeOrd;
            }
        }
        private void CloseOrder()
        {
            OrderFormVisibility = "Hidden";
        }

        private ICommand close;
        public ICommand CloseEmpty
        {
            get
            {
                if (close == null)
                    close = new RelayCommand(args => Close());
                return close;
            }
        }
        private void Close()
        {
            EmptyVisibility = "Hidden";
        }


        //Создание заказа
        private ICommand Indexx;
        public ICommand NewOrder
        {
            get
            {
                if (Indexx == null)
                    Indexx = new RelayCommand(args => CreateOrder());
                return Indexx;
            }
        }
        public void CreateOrder()
        {
            if (Point != null)
            {
                Shopping_Basket_Product.Clear();
                products.Clear();
                Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            }
            if (EmptyProducts.Count!=0)
            EmptyProducts.Clear();
            foreach (var i in Shopping_Basket_Product)
            {
                var product = crud.GetProduct(i.inventory_number);
              
                if (product.product_quantity < i.number || product.product_quantity == 0)
                {
                    EmptyProducts.Add(product);
                    i.number = product.product_quantity;
                    product.number = i.number;
                    crud.AddBumberBaketProd(i);
                }
            }
           
            if (EmptyProducts.Count != 0)
            {
                EmptyVisibility = "Visible";
                costOrder = 0;
                int count = 0;
                foreach (var i in Shopping_Basket_Product)
                {
                    var product = crud.GetProduct(i.inventory_number);
                    product.number = i.number;
                    if (product.number == 0) count++;
                    product.cost = product.cost * i.number;
                    products.Add(product);
                    costOrder = costOrder + product.cost;
                }

                //скидка
                customer = new Customer_Model();
                customer = crud.GetCustomer(UserId);
                sale = costOrder * customer.sale / 100;

                OrderCost = costOrder - sale;
                if (products.Count == 0)
                {
                    Visibility = "Visible";
                    ButtonVisibility = "Hidden";
                }
                else
                {
                    if (count != 0)
                        ButtonVisibility = "Hidden";
                    else
                        ButtonVisibility = "Visible";
                }
            }
            else
            {
                if (Point != null)
                {
                    TextVisibility = "Hidden";
                    order = new Order_Model();
                    order.customer_id = UserId;
                    order.pick_up_point_id = Point.pick_up_point_id;
                    order.status_id = 1;
                    order.total_cost = OrderCost;
                    foreach (var i in Shopping_Basket_Product)
                    {
                        var product = crud.GetProduct(i.inventory_number);
                        product.number = i.number;
                        product.cost = product.cost * i.number;
                        products.Add(product);
                        costOrder = costOrder + product.cost;
                    }

                    bool r = orderService.MakeOrder(order, products);
                    if (r)
                    {
                        foreach (var i in Shopping_Basket_Product)
                        {
                            crud.DeleteBasketProduct(i.id_basket);
                        }
                        Shopping_Basket_Product.Clear();
                        products.Clear();

                        Visibility = "Visible";
                        ButtonVisibility = "Hidden";
                        OrderFormVisibility = "Hidden";
                    }
                    else
                    {
                        Visibility = "Hidden";
                        ButtonVisibility = "Visible";

                    }
                }
                else TextVisibility = "Visible";
            }

            Messenger.Default.Send(new GenericMessage<Order_Model>(null));

        }

        //Обновление корзины
        private void UpdateBasket(GenericMessage<Shopping_Basket_Model> msg)
        {
            Shopping_Basket_Product.Clear();
            products.Clear();
            Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            costOrder = 0;
            int count = 0;
            foreach (var i in Shopping_Basket_Product)
            {                
                    var product = crud.GetProduct(i.inventory_number);                    
                    product.number = i.number;
                    if (product.number == 0) count++;
                    product.cost = product.cost * i.number;
                    products.Add(product);
                    costOrder = costOrder + product.cost;                
            }

            //скидка
            customer = new Customer_Model();
            customer = crud.GetCustomer(UserId);
            sale = costOrder * customer.sale / 100;

            OrderCost = costOrder - sale;
            if (products.Count == 0)
            {
                Visibility = "Visible";
                ButtonVisibility = "Hidden";
            }
            else
            {
                Visibility = "Hidden";
                if (count != 0)
                    ButtonVisibility = "Hidden";
                else
                    ButtonVisibility = "Visible";
            }
           
        }

        //Увеличиить количество товара в корзине
        private ICommand plus;
        public ICommand PlusNumber
        {
            get
            {
                if (plus == null)
                    plus = new RelayCommand(args => PlusClicked(args));
                return plus;
            }
        }
        private void PlusClicked(object args)
        {
            var prod = crud.GetProduct(Shopping_Basket_Product[(int)args].inventory_number);

            if (prod.product_quantity - Shopping_Basket_Product[(int)args].number > 0)
            {
                Shopping_Basket_Product[(int)args].number++;
                crud.AddBumberBaketProd(Shopping_Basket_Product[(int)args]);
            }
            else
            {
                var n = prod.product_quantity;
                Shopping_Basket_Product[(int)args].number = n;
                crud.AddBumberBaketProd(Shopping_Basket_Product[(int)args]);
            }
            Shopping_Basket_Product.Clear();
            products.Clear();
            Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            costOrder = 0;
            int count = 0;
            foreach (var i in Shopping_Basket_Product)
            {
                var product = crud.GetProduct(i.inventory_number);                
                product.number = i.number;
                if (product.number == 0) count++;
                product.cost = product.cost * i.number;
                products.Add(product);
                costOrder = costOrder + product.cost;
            }

            //скидка
            customer = new Customer_Model();
            customer = crud.GetCustomer(UserId);
            sale = costOrder * customer.sale / 100;

            OrderCost = costOrder - sale;
            if (products.Count == 0)
            {
                Visibility = "Visible";
                ButtonVisibility = "Hidden";
            }
            else
            {
                Visibility = "Hidden";
                if (count != 0)
                    ButtonVisibility = "Hidden";
                else
                    ButtonVisibility = "Visible";
            }
        }

        //Уменьшить количество товара в корзине
        private ICommand minus;
        public ICommand MinusNumber
        {
            get
            {
                if (minus == null)
                    minus = new RelayCommand(args => MinusClicked(args));
                return minus;
            }
        }
        private void MinusClicked(object args)
        {
           // var prod = crud.GetProduct(Shopping_Basket_Product[(int)args].inventory_number);

            if (Shopping_Basket_Product[(int)args].number > 1)
            {
                Shopping_Basket_Product[(int)args].number--;
                crud.AddBumberBaketProd(Shopping_Basket_Product[(int)args]);
            }
            Shopping_Basket_Product.Clear();
            products.Clear();
            Shopping_Basket_Product = crud.GetCustomersBaskets(UserId);
            costOrder = 0;
            int count = 0;
            foreach (var i in Shopping_Basket_Product)
            {
                var product = crud.GetProduct(i.inventory_number);                
                product.number = i.number;
                if (product.number == 0) count++;
                product.cost = product.cost * i.number;
                products.Add(product);
                costOrder = costOrder + product.cost;
            }

            //скидка
            customer = new Customer_Model();
            customer = crud.GetCustomer(UserId);
            sale = costOrder * customer.sale / 100;

            OrderCost = costOrder - sale;
            if (products.Count == 0)
            {
                Visibility = "Visible";
                ButtonVisibility = "Hidden";
            }
            else
            {
                Visibility = "Hidden";
                if (count != 0)
                    ButtonVisibility = "Hidden";
                else
                    ButtonVisibility = "Visible";

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}


