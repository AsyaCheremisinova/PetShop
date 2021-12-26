using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICRUD
    {
        List<Product_Model> GetAllProduct();
        Product_Model GetProduct(int id);
        List<Animals_Model> GetAllAnimals();
        List<Product_Type_Model> GetAllAProductType();
        List<Order_Model> GetAllOrders();
        List <Order_Model> GetCustomersOrder(int id);
        List<Order_Model> GetPeriodOrders(List<Order_Model> ord, DateTime Date1, DateTime Date2);
        Order_Model GetOrder(int id);
        List<Order_line_Model> GetAllOrder_Lines();
        List<Customer_Model> GetAllCustomers();
        Customer_Model GetCustomer(int id);
        List<Status_Model> GetAllStatus();
        List<Pick_Up_Point_Model> GetAllPoint();
        List<Type_Model> GetAllType();
        List<Shopping_Basket_Model> GetAllBaskets();
        List<Shopping_Basket_Model> GetCustomersBaskets(int id);
        Shopping_Basket_Model GetBasket(int id);
        void CreateProduct(Product_Model p);
        void UpdateProduct(Product_Model p);
        void DeleteProduct(int id);
        void DeleteBasketProduct(int id);
        void CreateProductBasket(Shopping_Basket_Model p);
        void AddBumberBaketProd(Shopping_Basket_Model s);
        void ChangeSale(Customer_Model c);      
        List<Order_Model> CustomersOrders(int id);


    }
}
