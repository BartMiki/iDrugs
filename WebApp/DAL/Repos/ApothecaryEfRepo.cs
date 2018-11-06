using Common;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class ApothecaryEfRepo
    {
        public iDrugsEntities GetContext => new iDrugsEntities();

        public IEnumerable<Apothecary> Get()
        {
            using (var db = new iDrugsEntities())
            {
                return db.Apothecaries.ToList();
            }
        }

        public Apothecary Get(int id)
        {
            using (var db = new iDrugsEntities())
            {
                return db.Apothecaries.Where(a => a.Id == id).FirstOrDefault();
            }
        }

        public void Add(string firstName, string lastName, decimal monthlySalary)
        {
            using (var db = new iDrugsEntities())
            {
                db.InsertApothecary(firstName, lastName, monthlySalary);
                db.SaveChanges();
            }
        }

        public void Fire(int id)
        {
            using (var db = new iDrugsEntities())
            {
                db.FireApothecary(id);
                db.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var db = new iDrugsEntities())
            {
                var toRemove = new Apothecary { Id = id };
                db.Apothecaries.Attach(toRemove);
                db.Apothecaries.Remove(toRemove);
                db.SaveChanges();
            }
        }

        public void Update(int id, string firstName, string lastName, decimal? monthlySalary)
        {
            using (var db = new iDrugsEntities())
            {
                var toUpdate = db.Apothecaries.Where(a => a.Id == id).FirstOrDefault();

                if(toUpdate != null)
                {
                    toUpdate.FirstName.TryUpdate(firstName);
                    toUpdate.LastName.TryUpdate(lastName);
                    toUpdate.MonthlySalary = monthlySalary ?? toUpdate.MonthlySalary;
                }
                db.SaveChanges();
            }
        }
    }
}
