using DAL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;
using DAL.EF;

namespace BLL
{
    public class CRUD: ICRUD
    {
        IDbRepos db;
        public CRUD(IDbRepos repos)
        {
            db = repos;
        }

        
        public List<Product_Model> GetAllProduct()
        {
            return db.Products.GetList().Select(i => new Product_Model(i)).ToList();
        }
        public Product_Model GetProduct(int id)
        {
            Product pr = db.Products.GetItem(id);
            Product_Model prod = new Product_Model(pr);
            return prod;

        }
       
        public List <Animals_Model> GetAllAnimals()
        {
            return db.Animals.GetList().Select(i => new Animals_Model(i)).ToList();
        }
        public List<Shopping_Basket_Model> GetAllBaskets()
        {
            return db.Baskets.GetList().Select(i => new Shopping_Basket_Model(i)).ToList();
        }
        public Shopping_Basket_Model GetBasket(int id)
        {
            Shopping_Basket pr = db.Baskets.GetItem(id);
            Shopping_Basket_Model prod = new Shopping_Basket_Model(pr);
            return prod;

        }
        public List<Product_Type_Model> GetAllAProductType()
        {
            return db.Product_Types.GetList().Select(i => new Product_Type_Model(i)).ToList();
        }
        public List<Order_Model> GetAllOrders()
        {
            return db.Orders.GetList().Select(i => new Order_Model(i)).ToList();
        }
        public List<Order_Model> GetCustomersOrder(int id)
        {
            var temp = GetAllOrders();
            List<Order_Model> result = new List<Order_Model>();
            foreach(var i in temp)
            {
                if (i.customer_id == id && i.status_id == 5)
                {
                    result.Add(i);
                }
            }
            return result;
        }
        public List<Shopping_Basket_Model> GetCustomersBaskets(int id)
        {
            var temp = GetAllBaskets();
            List<Shopping_Basket_Model> result = new List<Shopping_Basket_Model>();
            foreach (var i in temp)
            {
                if (i.customer_id == id)
                {
                    result.Add(i);
                }
            }
            return result;
        }
        public List<Order_Model> GetPeriodOrders(List<Order_Model> ord, DateTime Date1, DateTime Date2)
        {
            
            List<Order_Model> result = new List<Order_Model>();
            foreach (var i in ord)
            {
                if ( i.date >= Date1 && i.date <= Date2)
                {
                    result.Add(i);
                }
            }
            return result;
        }
        public Order_Model GetOrder(int id)
        {
            Order o = db.Orders.GetItem(id);
            Order_Model ord = new Order_Model(o);
            return ord;

        }
        public List<Customer_Model> GetAllCustomers()
        {
            return db.Customers.GetList().Select(i => new Customer_Model(i)).ToList();
        }
        public Customer_Model GetCustomer(int id)
        {           
                Customer c = db.Customers.GetItem(id);
                Customer_Model customer = new Customer_Model(c);
                return customer;           
        }
        public List<Status_Model> GetAllStatus()
        {
            return db.Statuses.GetList().Select(i => new Status_Model(i)).ToList();
        }
        public List<Pick_Up_Point_Model> GetAllPoint()
        {
            return db.Puck_Up_Points.GetList().Select(i => new Pick_Up_Point_Model(i)).ToList();
        }
        public List<Order_line_Model> GetAllOrder_Lines()
        {
            return db.Order_lines.GetList().Select(i => new Order_line_Model(i)).ToList();
        }

        public List<Type_Model> GetAllType()
        {
            return db.Types.GetList().Select(i => new Type_Model(i)).ToList();
        }


        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }

        public void CreateProduct(Product_Model p)
        {
            db.Products.Create(new Product() 
            {
                inventory_number = p.inventory_number,
                description = p.description,
                weight = p.weight,
                cost = p.cost,
                animal_id = p.animal_id,
                product_name = p.product_name,
                availability = p.availability,
                product_quantity = p.product_quantity,
                type_id = p.type_id,
                picture = p.picture
               
        });
            Save();
        }
        public void CreateProductBasket(Shopping_Basket_Model p)
        {
            db.Baskets.Create(new Shopping_Basket()
            {
                inventory_number = p.inventory_number,
                customer_id = p.customer_id,
                number = p.number,
               // id_basket = p.id_basket,               

            });
            Save();
        }

        public void UpdateProduct(Product_Model p)
        {
            Product pr = db.Products.GetItem(p.inventory_number);
            pr.product_name = p.product_name;
            pr.cost = p.cost;
            pr.animal_id = p.animal_id;
            pr.type_id = p.type_id;
            pr.availability = p.availability;
            pr.product_quantity = p.product_quantity;
            Save();
        }

        public void AddBumberBaketProd(Shopping_Basket_Model s)
        {
            Shopping_Basket sh = db.Baskets.GetItem(s.id_basket);
            sh.number = s.number;
            Save();
        }
        public void ChangeSale(Customer_Model c)
        {
            Customer customer = db.Customers.GetItem(c.customer_id);
            customer.sale = c.sale;
            Save();
        }
        public void DeleteProduct(int id)
        {
            Product p = db.Products.GetItem(id);
            if (p != null)
            {
                db.Products.Delete(p.inventory_number);
                Save();
            }
        }
        public void DeleteBasketProduct(int id)
        {
            Shopping_Basket b = db.Baskets.GetItem(id);
            if (b != null)
            {
                db.Baskets.Delete(b.id_basket);
                Save();
            }
        }

        public List<Order_Model> CustomersOrders(int id)
        {
            return db.CustomersOrders.GetCustomersOrders(id).Select(i => new Order_Model { order_id = i.order_id, date = i.date, status_name = i.status_name, customer_id = i.customer_id, pick_up_point_id = i.pick_up_point_id, status_id = i.status_id, total_cost = i.total_cost, point_name = i.point_name }).ToList();
        }

    }
}
