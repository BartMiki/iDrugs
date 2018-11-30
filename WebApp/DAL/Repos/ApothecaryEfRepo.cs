using Common.Utils;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using static Common.Utils.DatabaseExceptionHandler;

namespace DAL.Repos
{
    public class ApothecaryEfRepo : IApothecaryRepo
    {
        private readonly iDrugsEntities _context;

        public ApothecaryEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result<IEnumerable<Apothecary>> Get()
        {
            return Try(() => _context.Apothecaries.ToList().AsEnumerable());
        }

        public Result<Apothecary> Get(int id)
        {
            return Try(() => _context.Apothecaries.Where(a => a.Id == id).First());
        }

        public Result Add(Apothecary apothecary)
        {
            var result = Try(() =>
            {
                _context.InsertApothecary(apothecary.FirstName, apothecary.LastName, apothecary.MonthlySalary);
                _context.SaveChanges();
            });
            return result;
        }

        public Result Fire(int id)
        {
            var result = Try(() =>
            {
                _context.FireApothecary(id);
                _context.SaveChanges();
            });
            return result;
        }

        public Result Remove(int id)
        {
            var result = Try(() =>
            {
                var toRemove = new Apothecary { Id = id };
                _context.Apothecaries.Attach(toRemove);
                _context.Apothecaries.Remove(toRemove);
                _context.SaveChanges();
            });
            return result;
        }

        public Result<Apothecary> Update(Apothecary apothecary)
        {
            var result = Try(() =>
            {
                var entity = _context.Apothecaries.Find(apothecary.Id);

                if (entity == null) throw new Exception("Nie znaleziono aptekarza");

                if (entity.RowVersion != apothecary.RowVersion)
                    throw new DBConcurrencyException("Informacje o aptekarzu zostały zmienione, przez innego użytkownika");

                apothecary.RowVersion++;

                _context.Entry(entity)
                    .CurrentValues.SetValues(apothecary);

                _context.SaveChanges();
                return apothecary;
            });
            return result;
        }
    }
}
