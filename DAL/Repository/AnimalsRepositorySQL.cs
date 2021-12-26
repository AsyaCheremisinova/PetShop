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
   public class AnimalsRepositorySQL: IRepository<Animals>
    {
        private Pet_Shop db;
        public AnimalsRepositorySQL(Pet_Shop dbcontext)
        {
           this.db = dbcontext;
        }
        public List<Animals> GetList()
        {

            return db.Animals.ToList();
        }
        public Animals GetItem(int id)
        {
            return db.Animals.Find(id);
        }

        public void Create(Animals item)
        {
            db.Animals.Add(item);
        }

        public void Update(Animals item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Animals item = db.Animals.Find(id);
            if (item != null)
                db.Animals.Remove(item);
        }

        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
