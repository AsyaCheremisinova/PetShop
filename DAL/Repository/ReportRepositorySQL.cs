using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Interfaces;
using DAL.EF;


namespace DAL.Repository
{
    public class ReportRepositorySQL : IReportsRepository
    {
        private Pet_Shop db;

       
        public ReportRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }

        public List<TypeProd> Type_products()
        {
            Pet_Shop db = new Pet_Shop();
            var res = db.Type
                .Join(db.Animals, t => t.animal_id, a => a.animal_id, (t,a) => new {a.animal_name, t.type_id })
                .Join(db.Product_Type, an => an.type_id, p => p.type_id, (an,p) => new { an.animal_name, p.type_name })
                .Select(i => new TypeProd() { animal_name = i.animal_name , type_name = i.type_name })
             .ToList();
            return res;
        }

        public List<CatalogProd> GetProductCatalod( string product_type/*, string animal*/)
        {
            var result = db.Product
                              .Join(db.Animals, pr => pr.animal_id, cat => cat.animal_id, (pr, cat) => new { pr.inventory_number, pr.type_id, pr.product_name, pr.cost,  pr.picture, cat.animal_name })
                              .Join(db.Product_Type, pr => pr.type_id, t => t.type_id, (pr, t) => new {pr.inventory_number, pr.product_name, pr.cost, pr.picture, pr.animal_name, t.type_name })
                              .Where(i => i.type_name == product_type)
                              //.Where(i => i.animal_name == animal)
                              .Select(i => new CatalogProd { inventory_number = i.inventory_number, product_name = i.product_name, cost = i.cost, picture = i.picture })
                              .ToList();
            return result;
        }
        public List<HistoryProduct> GetHistoryProducts(int id)
        {
            var result = db.Order_line
                .Join(db.Product, o => o.inventory_number, p => p.inventory_number, (o, p) => new { o.order_id, o.order_line_id, o.order_line_cost, o.number, p.product_name, p.picture })
                .Where(i => i.order_id == id)
                .Select(i => new HistoryProduct { order_id = i.order_id, order_line_id = i.order_line_id, name = i.product_name, picture = i.picture, cost = i.order_line_cost, number = i.number })
                .ToList();
            return result;
        }
        public List<CustomersOrders> GetCustomersOrders(int id)
        {
            var result = db.Order
                .Join(db.Status, o => o.status_id, s => s.status_id, (o, s) => new { o.order_id, o.pick_up_point_id, o.status_id, o.total_cost, o.customer_id, s.status_name, o.date })
                .Join(db.Puck_Up_Point, o => o.pick_up_point_id, p => p.pick_up_point_id, (o,p) => new { o.order_id, o.pick_up_point_id, o.status_id, o.total_cost, o.customer_id, o.status_name, o.date, p.pick_up_point_name })
                .Where(i => i.customer_id == id)
                .Where(i => i.status_id != 5)
                .Select(i => new CustomersOrders { customer_id = id, total_cost = i.total_cost, status_id = i.status_id, status_name = i.status_name, date = i.date, order_id = i.order_id, pick_up_point_id = i.pick_up_point_id , point_name =i.pick_up_point_name })
                .ToList();
            return result;
        }
        public List<Prod> FindProduct(string name)        
        {
            var temp = db.Product.ToList();
            var result = db.Product.Where(i => i.product_name.Contains(name)).Select(j => new Prod() { product_name = j.product_name, cost = j.cost, inventory_number = j.inventory_number, picture = j.picture }).ToList();         
            return result;
            
        }

        public List<PopularProd> PopularProducts()
        {
            var result = db.Order_line
                .Join(db.Product, ol => ol.inventory_number, p => p.inventory_number, (ol, p) => new { ol.number, p.inventory_number, p.cost, p.product_name, p.picture })
                .Select(i => new PopularProd { inventory_number = i.inventory_number, picture = i.picture, product_name = i.product_name, cost = i.cost, number = i.number })
                .ToList();
            return result;
        }



        public bool CheckLogin(string login)
        {

            if (db.Customer.Where(i => login == i.customer_login).Count() == 0) return false;
            else return true;
        }

        public bool CheckPassword(string login, string password)
        {

            if (db.Customer.Where(i => login == i.customer_login && password == i.customer_password).Count() == 0) return false;
            else return true;
        }

        public int GetUserId(string login)
        {
            return db.Customer.Where(i => i.customer_login == login).ToList()[0].customer_id;
        }
    }
}
