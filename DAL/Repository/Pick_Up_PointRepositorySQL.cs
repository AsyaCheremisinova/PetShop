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
    public class Pick_Up_PointRepositorySQL: IRepository<Puck_Up_Point>
    {
        private Pet_Shop db;
        public Pick_Up_PointRepositorySQL(Pet_Shop dbcontext)
        {
            this.db = dbcontext;
        }
        public List<Puck_Up_Point> GetList()
        {
            return db.Puck_Up_Point.ToList();
        }
        public Puck_Up_Point GetItem(int id)
        {
            return db.Puck_Up_Point.Find(id);
        }

        public void Create(Puck_Up_Point item)
        {
            db.Puck_Up_Point.Add(item);
        }

        public void Update(Puck_Up_Point item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Puck_Up_Point item = db.Puck_Up_Point.Find(id);
            if (item != null)
                db.Puck_Up_Point.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
