using Common.Utils;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static Common.Utils.DatabaseExceptionHandler;

namespace DAL.Repos
{
    public class ApothecaryEfRepo : IApothecaryRepo
    {
        public iDrugsEntities Context => new iDrugsEntities();

        public Result<IEnumerable<Apothecary>> Get()
        {
            var result = Try(() =>
            {
                using (var db = new iDrugsEntities())
                {
                    return db.Apothecaries.ToList().AsEnumerable();
                }
            });
            return result;
        }

        public Result<Apothecary> Get(int id)
        {
            var result = Try(() =>
            {
                using (var db = new iDrugsEntities())
                {
                    return db.Apothecaries.Where(a => a.Id == id).First();
                }
            });
            return result;
        }

        public Result Add(Apothecary apothecary)
        {
            var result = Try(() =>
            {
                using (var db = new iDrugsEntities())
                {
                    db.InsertApothecary(apothecary.FirstName, apothecary.LastName, apothecary.MonthlySalary);
                    db.SaveChanges();
                }
            });
            return result;
        }

        public Result Fire(int id)
        {
            var result = Try(() =>
            {
                using (var db = new iDrugsEntities())
                {
                    db.FireApothecary(id);
                    db.SaveChanges();
                }
            });
            return result;
        }

        public Result Remove(int id)
        {
            var result = Try(() =>
            {
                using (var db = Context)
                {
                    var toRemove = new Apothecary { Id = id };
                    db.Apothecaries.Attach(toRemove);
                    db.Apothecaries.Remove(toRemove);
                    db.SaveChanges();
                }
            });
            return result;
        }

        public Result<Apothecary> Update(Apothecary apothecary)
        {
            var result = Try(() =>
            {
                using (var db = new iDrugsEntities())
                {
                    var toUpdate = db.Apothecaries.Where(x => x.Id == apothecary.Id).FirstOrDefault();

                    if (toUpdate != null)
                    {
                        toUpdate.FirstName = apothecary.FirstName ?? toUpdate.FirstName;
                        toUpdate.LastName = apothecary.LastName ?? toUpdate.LastName;
                        toUpdate.MonthlySalary = apothecary.MonthlySalary;
                    }
                    db.SaveChanges();
                    return toUpdate;
                }
            });
            return result;
        }
    }
}
