using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

        public Result<string> AcceptCreated(int id)
        {
            return Try(() =>
            {
                var sb = new StringBuilder();
                _context.AcceptCreatedPrescription(id);

                sb.AppendLine("Szanowny kliencie,")
                    .AppendLine($"Twoja recepta została zarejestrowana w naszym systemie pod numerem {id}")
                    .AppendLine("Dziękujemy za rejestrację i zapraszamy do realizacji.")
                    .AppendLine()
                    .AppendLine("Pozdrawiamy,")
                    .AppendLine("Zespół iDrugs");

                return sb.ToString();

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

        public Result<string> BuyAll(int id)
        {
            var result = _context.BeginTransaction(() =>
            {
                var prescription = _context.Prescriptions
                    .Include(x => x.PrescriptionItems)
                    .FirstOrDefault(x => x.Id == id);

                if (prescription == null) throw new PrescriptionNotFoundException(id);

                var items = prescription.PrescriptionItems.Where(x => !x.Status.Equals("BOUGHT"));

                var sb = new StringBuilder()
                    .AppendLine("Szanowny kliencie,")
                    .AppendLine($"Twoja recepta o numerze {prescription.Id} została właśnie wykupiona. Poniżej przedstawiamy kupione leki:");

                var totalCost = 0.0M;

                foreach (var item in items)
                {
                    var toBuy = item.QuantityToBuy - item.QuantityAlreadyBought;

                    var stock = _context.DrugStoreAvailableMedicines.FirstOrDefault(x => x.MedicineId == item.MedicineId);
                    if (stock == null) throw new Exception($"Brakuje {toBuy} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    if (stock.Quantity < toBuy) throw new Exception($"Brakuje {toBuy - stock.Quantity} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    sb.AppendLine($"\t{item.Medicine.Name} - ilość {item.QuantityToBuy}, cena jednostkowa {item.Medicine.UnitPrice:0.00} zł");
                    totalCost += item.QuantityToBuy * item.Medicine.UnitPrice;

                    _context.Database.ExecuteSqlCommand("UPDATE PrescriptionItem SET QuantityAlreadyBought = QuantityToBuy WHERE Id = @id",
                        new SqlParameter("id", item.Id));

                    _context.Database.ExecuteSqlCommand("UPDATE DrugStoreAvailableMedicine SET Quantity = (Quantity - @toBuy) WHERE MedicineId = @medId",
                        new SqlParameter("toBuy", toBuy),
                        new SqlParameter("medId", item.MedicineId));
                }

                sb.AppendLine($"Całkowity koszt wyniósł {totalCost:0.00} zł")
                    .AppendLine("Dziękujemy za zakupy i zapraszamy ponownie")
                    .AppendLine()
                    .AppendLine("Pozdrawiamy,")
                    .AppendLine("Zespół iDrugs");

                return sb.ToString();
            }, GetType());

            return result;
        }

        public Result<string> BuySome(int prescriptionId, IEnumerable<(int itemId, int amount)> itemsToBuy)
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

                var sb = new StringBuilder()
                    .AppendLine("Szanowny kliencie,");

                var receiptWillBeCompleted = prescription.PrescriptionItems.Where(x => x.Status != "BOUGHT").Count() == items.Count()
                    && items.Where(x => x.item.Status != "BOUGHT").All(x => x.item.QuantityToBuy == x.amount);

                if (receiptWillBeCompleted)
                {
                    sb.AppendLine($"Twoja recepta o numerze {prescription.Id} została właśnie wykupiona. Poniżej przedstawiamy kupione leki:");
                }
                else
                {
                    sb.AppendLine($"Z Twojej recepty o numerze {prescription.Id} została zakupina część leków. Poniżej przedstawiamy kupione leki:");
                }

                var totalCost = 0.0M;

                foreach ((PrescriptionItem item, int amount) in items)
                {
                    var remainingToBuy = item.QuantityToBuy - item.QuantityAlreadyBought;

                    if (amount > remainingToBuy) throw new Exception(
                        $"Nie można kupić więcej leku {item.Medicine.Name} niż zostało do wykupienia. " +
                        $"Chciano kupić {amount}, a zostało do kupienia {remainingToBuy}");

                    var stock = _context.DrugStoreAvailableMedicines.FirstOrDefault(x => x.MedicineId == item.MedicineId);
                    if (stock == null) throw new Exception($"Brakuje {amount} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    if (stock.Quantity < amount) throw new Exception($"Brakuje {amount - stock.Quantity} sztuk leku {item.Medicine.Name} o ID {item.MedicineId} na stanie apteki");

                    totalCost += amount * item.Medicine.UnitPrice;

                    _context.Database.ExecuteSqlCommand("UPDATE PrescriptionItem SET QuantityAlreadyBought = (QuantityAlreadyBought + @amount) WHERE Id = @id",
                        new SqlParameter("id", item.Id),
                        new SqlParameter("amount", amount));

                    _context.Database.ExecuteSqlCommand("UPDATE DrugStoreAvailableMedicine SET Quantity = (Quantity - @toBuy) WHERE MedicineId = @medId",
                        new SqlParameter("toBuy", amount),
                        new SqlParameter("medId", item.MedicineId));
                }

                sb.AppendLine($"Całkowity koszt wyniósł {totalCost:0.00} zł");

                if (!receiptWillBeCompleted) sb.Append("Pamiętaj, że Twoja recepta nadal nie jest zakończona i znajdują się na niej leki które należy kupić");

                    sb.AppendLine("Dziękujemy za zakupy i zapraszamy ponownie")
                    .AppendLine()
                    .AppendLine("Pozdrawiamy,")
                    .AppendLine("Zespół iDrugs");

                return sb.ToString();

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
