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
    public class StatusRepositorySQL: IRepository<Status>
    {
        private Pet_Shop db;
        public StatusRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Status> GetList()
        {
            return db.Status.ToList();
        }
        public Status GetItem(int id)
        {
            return db.Status.Find(id);
        }

        public void Create(Status item)
        {
            db.Status.Add(item);
        }

        public void Update(Status item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Status item = db.Status.Find(id);
            if (item != null)
                db.Status.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
