using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Data.Entity;
using DAL.EF;


namespace DAL.Repository
{
    public class ProductRepositorySQL: IRepository<Product>
    {
        private Pet_Shop db;
        public ProductRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Product> GetList()
        {
            return db.Product.ToList();
        }

        public Product GetItem(int id)
        {
            return db.Product.Find(id);
        }


        public void Create(Product item)
        {
            db.Product.Add(item);
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product item = db.Product.Find(id);
            if (item != null)
                db.Product.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }

    }
}
