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
    public class Order_lineRepositorySQL: IRepository<Order_line>
    {
        private Pet_Shop db;
        public Order_lineRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Order_line> GetList()
        {
            return db.Order_line.ToList();
        }
        public Order_line GetItem(int id)
        {
            return db.Order_line.Find(id);
        }

        public void Create(Order_line item)
        {
            db.Order_line.Add(item);
        }

        public void Update(Order_line item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order_line item = db.Order_line.Find(id);
            if (item != null)
                db.Order_line.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
