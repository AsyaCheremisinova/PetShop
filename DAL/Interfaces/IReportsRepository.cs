using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface IReportsRepository
    {
        List<TypeProd> Type_products();
        List<CatalogProd> GetProductCatalod(string prduct_type/*, string animal*/);
        List<HistoryProduct> GetHistoryProducts(int id);
        List<CustomersOrders> GetCustomersOrders(int id);
        List<Prod> FindProduct(string name);
        List<PopularProd> PopularProducts();
        bool CheckLogin(string login);
        bool CheckPassword(string login, string password);
        int GetUserId(string login);
    }
        
}
