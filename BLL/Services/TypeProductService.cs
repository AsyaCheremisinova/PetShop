using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Models;
using BLL.Interfaces;
using System.Collections.ObjectModel;


namespace BLL.Services
{
    public class TypeProductService : ITypeProductService
    {
        IDbRepos dataBase;
        public TypeProductService(IDbRepos dbRepository)
        {
            dataBase = dbRepository;
        }
        public ObservableCollection<Tree_Model> GetAnimalsTypeModels()
        {
            var Animals = dataBase.Animals.GetList().Select(i => new Animals_Model { animal_id = i.animal_id, animal_name = i.animal_name }).ToList();
            var result = dataBase.Type_product.Type_products().ToList();

            ObservableCollection<Tree_Model> tempModel = new ObservableCollection<Tree_Model>();
            foreach (var animal in Animals)
            {
                Tree_Model temp = new Tree_Model();
                ObservableCollection<Product_Type_Model> type_names = new ObservableCollection<Product_Type_Model>();

                temp.animal_name = animal.animal_name;
                var types = result.Where(i => i.animal_name == animal.animal_name).Select(i => new Product_Type_Model { type_name = i.type_name }).ToList();
                foreach (var i in types)
                {
                    type_names.Add(i);
                }
                temp.type_name = type_names;

                tempModel.Add(temp);
            }

            return tempModel;
        }

        public List<Product_Model> GetProductCatalod(string prduct_type/*, string animal*/)
        {
            return dataBase.Catalog_product.GetProductCatalod(prduct_type/*, animal*/).Select(i => new Product_Model { inventory_number = i.inventory_number, product_name = i.product_name, cost = i.cost, picture = i.picture }).ToList();
        }

        public List<Product_Model> FindProduct(string name)
        {
            return dataBase.Catalog_product.FindProduct(name).Select(i => new Product_Model { inventory_number = i.inventory_number, product_name = i.product_name, cost = i.cost, picture = i.picture }).ToList();
        }

        public List<Product_Model> PopularProducts()
        {
            var res = dataBase.PopularProducts.PopularProducts().Select(i => new Product_Model { inventory_number = i.inventory_number, cost = i.cost, picture = i.picture, product_name = i.product_name, number = i.number }).ToList();
            List<Product_Model> pop = new List<Product_Model>();
            foreach (var i in res)
            {
                if (pop.Count != 0)
                {
                    int count = 0;
                    for (int j = 0; j< pop.Count; j++)
                    {
                        if (i.inventory_number == pop[j].inventory_number)
                        {
                            pop[j].count++;
                            pop[j].number += i.number;
                            count++;
                        }                        
                    }
                    if(count ==0)
                    {
                        pop.Add(i);
                    }
                }
                else
                {
                    pop.Add(i);
                    pop[0].count = 1;
                }              
            }
            pop = pop.OrderBy(i => -i.number).ToList();

            return pop;
        }
    }
}
