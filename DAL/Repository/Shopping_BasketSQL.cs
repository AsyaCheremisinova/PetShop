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
    public class Shopping_BasketSQL : IRepository<Shopping_Basket>
    {
        private Pet_Shop db;
        public Shopping_BasketSQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Shopping_Basket> GetList()
        {
            return db.Shopping_Basket.ToList();
        }
        public Shopping_Basket GetItem(int id)
        {
            return db.Shopping_Basket.Find(id);
        }

        public void Create(Shopping_Basket item)
        {
            db.Shopping_Basket.Add(item);
        }

        public void Update(Shopping_Basket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Shopping_Basket item = db.Shopping_Basket.Find(id);
            if (item != null)
                db.Shopping_Basket.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
