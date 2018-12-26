using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.DoctorModels;
using WebApp.Models.MedicineModels;

namespace WebApp.Interfaces
{
    public interface ISelectService
    {
        Result<IEnumerable<MedicineSelectModel>> GetMedicineSelectList();
        Result<IEnumerable<DoctorSelectViewModel>> GetDoctorSelectList();
        Result<IEnumerable<ApothecarySelectViewModel>> GetApothecarySelectList();
    }
}
