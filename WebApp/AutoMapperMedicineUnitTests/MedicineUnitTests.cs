using System;
using AutoMapper;
using DAL;
using DAL.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Models;
using static WebApp.AutoMapperProfiels;
using Shouldly;
using Common.Utils;

namespace AutoMapperMedicineUnitTests
{
    [TestClass]
    public class MedicineUnitTests
    {
        static MedicineUnitTests()
        {
            CreateMaps();
        }

        [TestMethod]
        public void MedicineViewModelToMedicine_AllProperties_Success()
        {
            var mvm = GetFakeMedicineViewModel();
            var m = Mapper.Map<Medicine>(mvm);

            CompareMedicines(m, mvm);
        }

        [TestMethod]
        public void MedicineViewModelToMedicine_NoMedicineType_Success()
        {
            var mvm = GetFakeMedicineViewModel();
            mvm.MedicineType = null;

            var m = Mapper.Map<Medicine>(mvm);

            CompareMedicines(m, mvm);
        }

        public static void CompareMedicines(MedicineViewModel mvm, Medicine m) => CompareMedicines(m, mvm);

        public static void CompareMedicines(Medicine m, MedicineViewModel mvm)
        {
            m.Amount.ShouldBe(mvm.Amount);
            m.Expired.ShouldBe(mvm.Expired);
            m.Id.ShouldBe(mvm.Id);
            m.Name.ShouldBe(mvm.Name);
            m.Refund.ShouldBe(mvm.Refund);
            m.UnitPrice.ShouldBe(mvm.UnitPrice);

            var mt = m.MedicineType;
            if(mt != null && mvm.MedicineType != null)
            {
                m.MedicineTypeId.ShouldBe(mvm.MedicineType.Id);

                mt.Id.ShouldBe(mvm.MedicineType.Id);
                mt.MedType.ShouldBe(mvm.MedicineType.MedType.AsDatabaseType());
                mt.Unit.ShouldBe(mvm.MedicineType.Unit.AsDatabaseType());
            }
            else
            {
                m.MedicineTypeId.ShouldBe(0);
            }
        }

        public static MedicineViewModel GetFakeMedicineViewModel()
        {
            var mvm = new MedicineViewModel
            {
                Amount = 10,
                Expired = false,
                Id = 1,
                MedicineType = new MedicineTypeViewModel
                {
                    MedType = MedType.Liquid,
                    Id = 5,
                    Unit = Unit.Mililiters
                },
                Name = "Xenna Extra Forte Plus",
                Refund = 0.95M,
                UnitPrice = 19.99M
            };
            return mvm;
        }
    }
}
