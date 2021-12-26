using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.EF;
using Type = DAL.EF.Type;

namespace DAL.Repository
{
    public class DBReposSQL: IDbRepos
    {
        private Pet_Shop db;
        private AnimalsRepositorySQL animalsRepository;
        private CustomerRepositorySQL customerRepository;
        private OrderRepositorySQL orderRepository;
        private Pick_Up_PointRepositorySQL pick_Up_PointRepository;
        private ProductRepositorySQL productRepository;
        private Product_TypeRepositorySQL product_TypeRepository;
        private ReportRepositorySQL reportRepository;
        private StatusRepositorySQL statusRepository;
        private Order_lineRepositorySQL order_LineRepository;
        private TypeRepositorySQL typeRepository;
        private Shopping_BasketSQL basketRepository;

        public DBReposSQL()
        {
            db = new Pet_Shop();
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepositorySQL(db);
                return productRepository;
            }
        }
        public IRepository<Animals> Animals
        {
            get
            {
                if (animalsRepository == null)
                    animalsRepository = new AnimalsRepositorySQL(db);
                return animalsRepository;
            }
        }
        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepositorySQL(db);
                return customerRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepositorySQL(db);
                return orderRepository;
            }
        }
        public IRepository<Puck_Up_Point> Puck_Up_Points
        {
            get
            {
                if (pick_Up_PointRepository == null)
                    pick_Up_PointRepository = new Pick_Up_PointRepositorySQL(db);
                return pick_Up_PointRepository;
            }
        }
        public IRepository<Product_Type> Product_Types
        {
            get
            {
                if (product_TypeRepository == null)
                    product_TypeRepository = new Product_TypeRepositorySQL(db);
                return product_TypeRepository;
            }
        }
        public IRepository<Type> Types
        {
            get
            {
                if (typeRepository == null)
                    typeRepository = new TypeRepositorySQL(db);
                return typeRepository;
            }
        }
        public IRepository<Shopping_Basket> Baskets
        {
            get
            {
                if (basketRepository == null)
                    basketRepository = new Shopping_BasketSQL(db);
                return basketRepository;
            }
        }
        public IRepository<Status> Statuses
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new StatusRepositorySQL(db);
                return statusRepository;
            }
        }
        public IRepository<Order_line> Order_lines
        {
            get
            {
                if (order_LineRepository == null)
                    order_LineRepository = new Order_lineRepositorySQL(db);
                return order_LineRepository;
            }
        }
        public IReportsRepository Type_product
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }
        public IReportsRepository Catalog_product
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }
        public IReportsRepository HistoryProduct
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }
        public IReportsRepository CustomersOrders
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }
        public IReportsRepository AuthorizationService
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }
        public IReportsRepository PopularProducts
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }


        public int Save()
        {

            return db.SaveChanges();
        }
    }
}
