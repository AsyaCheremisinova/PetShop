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
    public class Product_TypeRepositorySQL : IRepository<Product_Type>
    {
        private Pet_Shop db;
        public Product_TypeRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Product_Type> GetList()
        {
            return db.Product_Type.ToList();
        }
        public Product_Type GetItem(int id)
        {
            return db.Product_Type.Find(id);
        }

        public void Create(Product_Type item)
        {
            db.Product_Type.Add(item);
        }

        public void Update(Product_Type item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product_Type item = db.Product_Type.Find(id);
            if (item != null)
                db.Product_Type.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
