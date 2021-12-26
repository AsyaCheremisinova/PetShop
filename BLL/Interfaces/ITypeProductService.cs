using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;


namespace BLL.Interfaces
{
    public interface ITypeProductService
    {
        ObservableCollection<Tree_Model> GetAnimalsTypeModels();
        List <Product_Model> GetProductCatalod(string prduct_type/*, string animal*/);
        List<Product_Model> FindProduct(string name);
        List<Product_Model> PopularProducts();


    }
}
