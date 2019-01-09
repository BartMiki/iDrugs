using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace DAL.Repos
{
    public class PrescriptionEfRepo : IPrescriptionRepo
    {
        private readonly iDrugsEntities _context;

        public PrescriptionEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result AcceptCreated(int id)
        {
            return Try(() =>
            {
                _context.AcceptCreatedPrescription(id);
            }, typeof(PrescriptionEfRepo));
        }

        public Result<int> AddPrescription(Prescription entity)
        {
            return Try(() =>
            {
                var result = _context.AddPrescription(entity.DoctorId, entity.ApothecaryId, entity.PrescriptionDate, entity.Email);
                _context.SaveChanges();
                return result.FirstOrDefault().Value;
            }, typeof(PrescriptionEfRepo));
        }

        public Result AddPrescriptionItem(int prescriptionId, PrescriptionItem item)
        {
            return Try(() =>
            {
                var entity = _context.Prescriptions.Find(prescriptionId);

                if (entity == null) throw new PrescriptionNotFoundException(prescriptionId);

                _context.AddPrescriptionItem(item.MedicineId, item.PrescriptionId, item.QuantityToBuy);
                _context.SaveChanges();

            }, typeof(PrescriptionEfRepo));
        }

        public Result Buy(Prescription prescription)
        {
            throw new System.NotImplementedException();
        }

        public Result BuyAll(int id)
        {
            var result = _context.BeginTransaction(() => 
            {
                var prescription = _context.Prescriptions
                    .Include(x => x.PrescriptionItems)
                    .FirstOrDefault(x => x.Id == id);

                if (prescription == null) throw new PrescriptionNotFoundException(id);

                foreach(var item in prescription.PrescriptionItems)
                {
                    var toBuy = item.QuantityToBuy - item.QuantityAlreadyBought;

                    var stock = _context.DrugStoreAvailableMedicines.FirstOrDefault(x => x.MedicineId == item.MedicineId);
                    if (stock == null) throw new Exception($"Brakuje {toBuy} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    if(stock.Quantity < toBuy) throw new Exception($"Brakuje {toBuy - stock.Quantity} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    _context.Database.ExecuteSqlCommand("UPDATE PrescriptionItem SET QuantityAlreadyBought = QuantityToBuy WHERE Id = @id",
                        new SqlParameter("id",item.Id));

                    _context.Database.ExecuteSqlCommand("UPDATE DrugStoreAvailableMedicine SET Quantity = (Quantity - @toBuy) WHERE MedicineId = @medId",
                        new SqlParameter("toBuy", toBuy),
                        new SqlParameter("medId", item.MedicineId));
                }

            }, GetType());

            return result;
        }

        public Result BuySome(int prescriptionId, IEnumerable<(int itemId, int amount)> itemsToBuy)
        {
            return _context.BeginTransaction(() => 
            {
                var prescription = _context.Prescriptions
                    .Include(x => x.PrescriptionItems)
                    .FirstOrDefault(x => x.Id == prescriptionId);

                if (prescription == null) throw new PrescriptionNotFoundException(prescriptionId);

                var items = from item in prescription.PrescriptionItems
                            join itemToBuy in itemsToBuy
                            on item.Id equals itemToBuy.itemId
                            select (item, itemToBuy.amount);


                foreach ((PrescriptionItem item, int amount) in items)
                {
                    var remainingToBuy = item.QuantityToBuy - item.QuantityAlreadyBought;

                    if (amount > remainingToBuy) throw new Exception(
                        $"Nie można kupić więcej leku {item.Medicine.Name} niż zostało do wykupienia. " +
                        $"Chciano kupić {amount}, a zostało do kupienia {remainingToBuy}");

                    var stock = _context.DrugStoreAvailableMedicines.FirstOrDefault(x => x.MedicineId == item.MedicineId);
                    if (stock == null) throw new Exception($"Brakuje {amount} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    if (stock.Quantity < amount) throw new Exception($"Brakuje {amount - stock.Quantity} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    _context.Database.ExecuteSqlCommand("UPDATE PrescriptionItem SET QuantityAlreadyBought = (QuantityAlreadyBought + @amount) WHERE Id = @id",
                        new SqlParameter("id", item.Id),
                        new SqlParameter("amount", amount));

                    _context.Database.ExecuteSqlCommand("UPDATE DrugStoreAvailableMedicine SET Quantity = (Quantity - @toBuy) WHERE MedicineId = @medId",
                        new SqlParameter("toBuy", amount),
                        new SqlParameter("medId", item.MedicineId));
                }

            }, GetType());
        }

        public Result DeletePrescription(int id)
        {
            return _context.BeginTransaction(() =>
            {
                var parameter = new SqlParameter("id", id);

                _context.Database.ExecuteSqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @id", parameter);
                _context.Database.ExecuteSqlCommand("DELETE FROM Prescription WHERE Id = @id", parameter);

            }, typeof(PrescriptionEfRepo));
        }

        public Result DeletePrescriptionItem(int prescriptionId, int itemId)
        {
            return Try(() =>
            {
                var id = new SqlParameter("id", itemId);
                var pId = new SqlParameter("pId", prescriptionId);

                _context.Database.ExecuteSqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @pId AND Id = @id", pId, id);

            }, typeof(PrescriptionEfRepo));
        }

        public Result EditPrescription(Prescription prescription)
        {
            return Try(() =>
            {
                var entity = _context.Prescriptions.Find(prescription.Id);

                if (entity == null) throw new PrescriptionNotFoundException(prescription.Id);

                _context.Entry(entity)
                    .CurrentValues.SetValues(prescription);

                _context.SaveChanges();
            }, typeof(PrescriptionEfRepo));
        }

        public Result EditPrescriptionItem(PrescriptionItem item)
        {
            return Try(() =>
            {
                var entity = _context.PrescriptionItems.Find(item.Id);

                if (entity == null) throw new PrescriptionItemNotFoundException(item.PrescriptionId, item.Id);

                _context.ChangePrescriptedAmount(item.Id, item.QuantityToBuy, item.RowVersion);

                _context.SaveChanges();
            }, typeof(PrescriptionEfRepo));
        }

        public Result<IEnumerable<Prescription>> Get()
        {
            return Try(() =>
            {
                return _context.Prescriptions.AsEnumerable();
            }, typeof(PrescriptionEfRepo));
        }

        public Result<Prescription> Get(int id)
        {
            return Try(() =>
            {
                var entity = _context.Prescriptions
                    .Include(x => x.Apothecary)
                    .Include(x => x.Doctor)
                    .Include(x => x.PrescriptionItems)
                    .FirstOrDefault(x => x.Id == id);

                if (entity == null) throw new PrescriptionNotFoundException(id);

                return entity;
            }, typeof(PrescriptionEfRepo));
        }

        public Result<PrescriptionItem> GetItem(int id)
        {
            return Try(() =>
            {
                var entity = _context.PrescriptionItems.Find(id);

                if (entity == null) throw new PrescriptionItemNotFoundException(id);

                return entity;

            }, typeof(PrescriptionEfRepo));
        }
    }
}
