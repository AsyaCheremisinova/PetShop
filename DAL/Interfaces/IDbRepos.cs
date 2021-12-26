using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface IDbRepos
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<Animals> Animals { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Order_line> Order_lines { get; }
        IRepository<Product_Type> Product_Types { get; }
        IRepository<Puck_Up_Point> Puck_Up_Points { get; }
        IRepository<Status> Statuses { get; }
        IRepository<EF.Type> Types { get; }
        IRepository<Shopping_Basket> Baskets { get; }
        IReportsRepository Type_product { get; }
        IReportsRepository Catalog_product { get; }
        IReportsRepository HistoryProduct { get; }
        IReportsRepository CustomersOrders { get; }
        IReportsRepository AuthorizationService { get; }
        IReportsRepository PopularProducts { get; }

        int Save();
    }
}
