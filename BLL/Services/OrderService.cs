using DAL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;
using DAL.EF;


namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private IDbRepos db;
        public OrderService(IDbRepos repos)
        {
            db = repos;
        }
        public bool MakeOrder(Order_Model orderDto, ObservableCollection<Product_Model> products /*List<int> items*//*, Order_line_Model lines*/)
        {
            List<Order_line> orderproduct = new List<Order_line>();
            decimal? sum = 0;
            int n = 0;
            while (n < products.Count)
            {
                foreach (var pId in products)
                {

                    Product product = db.Products.GetItem(pId.inventory_number);
                    if (product == null || product.product_quantity == 0)
                        return false;
                    sum += product.cost;
                    if (product.product_quantity > 0)
                    {
                        product.product_quantity = (int)(product.product_quantity - 1 * pId.number);
                    }
                    else { return false; }
                    Order_line OrderLine = new Order_line
                    {
                        // order_line_id = lines.order_line_id + n,
                        order_line_cost = product.cost * (decimal)pId.number,
                        number = (int)pId.number,
                        order_id = orderDto.order_id,
                        inventory_number = product.inventory_number,
                    };
                    n++;
                    db.Order_lines.Create(OrderLine);
                    orderproduct.Add(OrderLine);
                }
            }

            //sum = new Discount(0.1m).GetDiscountedPrice((decimal)sum);

            Order order = new Order
            {
                //order_id = orderDto.order_id,
                date = DateTime.Now,
                pick_up_point_id = orderDto.pick_up_point_id,
                total_cost = orderDto.total_cost,
                customer_id = orderDto.customer_id,
                status_id = orderDto.status_id
            };
            db.Orders.Create(order);

            if (db.Save() > 0)
                return true;
            return false;

        }
    }
}
