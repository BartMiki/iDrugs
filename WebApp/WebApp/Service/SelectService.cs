using Common.Utils;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using WebApp.Models.MedicineModels;
using static Common.Handlers.StaticDatabaseExceptionHandler;
using static AutoMapper.Mapper;
using WebApp.Interfaces;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.DoctorModels;
using System.Linq;

namespace WebApp.Service
{
    public class SelectService : ISelectService
    {
        private readonly IMedicineRepo _medicineRepo;
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly IDoctorRepo _doctorRepo;

        public SelectService(IMedicineRepo medicineRepo, IApothecaryRepo apothecaryRepo, IDoctorRepo doctorRepo)
        {
            _medicineRepo = medicineRepo;
            _apothecaryRepo = apothecaryRepo;
            _doctorRepo = doctorRepo;
        }

        public Result<IEnumerable<ApothecarySelectViewModel>> GetApothecarySelectList()
        {
            return Try(() =>
            {
                var result = _apothecaryRepo.Get();

                if (!result.IsSuccess) throw new Exception(result.FailureMessage);

                var temp = Map<IEnumerable<ApothecaryViewModel>>(result.Value.Where(x => !x.FireDate.HasValue));
                return Map<IEnumerable<ApothecarySelectViewModel>>(temp);
            }, GetType());
        }

        public Result<IEnumerable<DoctorSelectViewModel>> GetDoctorSelectList()
        {
            return Try(() =>
            {
                var result = _doctorRepo.Get();

                if (!result.IsSuccess) throw new Exception(result.FailureMessage);

                return Map<IEnumerable<DoctorSelectViewModel>>(result.Value.Where(x => x.HasValidMedicalLicense));
            }, GetType());
        }

        public Result<IEnumerable<MedicineSelectModel>> GetMedicineSelectList()
        {
            return Try(() =>
            {
                var result = _medicineRepo.Get();

                if (!result.IsSuccess) throw new Exception(result.FailureMessage);

                var temp = Map<IEnumerable<MedicineViewModel>>(result.Value);
                return Map<IEnumerable<MedicineSelectModel>>(temp);
            }, GetType());
        }
    }
}
