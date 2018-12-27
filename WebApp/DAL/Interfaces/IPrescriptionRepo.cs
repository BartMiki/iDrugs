using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPrescriptionRepo
    {
        #region Get Methods
        Result<IEnumerable<Prescription>> Get();
        Result<Prescription> Get(int id);
        Result<PrescriptionItem> GetItem(int id);
        #endregion
        #region Stored Procedures
        Result AcceptCreated(int id);
        #endregion
        #region Add Methods
        Result<int> AddPrescription(Prescription entity);
        Result AddPrescriptionItem(int prescriptionId, PrescriptionItem item);
        #endregion
        #region Edit Methods
        Result EditPrescriptionItem(PrescriptionItem item);
        Result EditPrescription(Prescription prescription);
        #endregion
        #region Delete Methods
        Result DeletePrescription(int id);
        Result DeletePrescriptionItem(int prescriptionId, int itemId);
        #endregion
        #region Buy
        Result BuyAll(int id);
        Result Buy(Prescription prescription);
        #endregion
    }
}
