using AutoMapper;
using Common.Utils;
using DAL;
using DAL.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using WebApp.Models.MedicineModels;
using WebApp.Models.OrderModels;
using static WebApp.AutoMapperProfiels;

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
        public void MedicineToMedicineViewModel_AllProperties_Success()
        {
            var copy = GetFakeMedicineViewModel();
            var m = Mapper.Map<Medicine>(copy);
            var mvm = Mapper.Map<MedicineViewModel>(m);

            CompareMedicines(m, mvm);
        }

        [TestMethod]
        public void CraeteMedicineViewModelToMedicineViewModel()
        {
            var create = new CreateMedicineViewModel
            {
                Amount = 1,
                MedType = MedType.Liquid,
                Name = "Test medicine",
                RefundString = "25%",
                Unit = Unit.Grams,
                UnitPrice = 100
            };

            var m = Mapper.Map<Medicine>(create);

            return;
        }

        [TestMethod]
        public void OrderToModel()
        {
            var copy = GetFakeMedicineViewModel();
            var m = Mapper.Map<Medicine>(copy);

            var order = new Order
            {
                Id = 1,
            };

            var orderItem = new OrderItem
            {
                Id = 1,
                Medicine = m,
                MedicineId = m.Id,
                Order = order,
                OrderId = order.Id,
                Quantity = 10
            };

            order.OrderItems = null;//new []{ orderItem };

            var res = Mapper.Map<OrderDetailViewModel>(order);


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
            m.MedType.ShouldBe(mvm.MedType.AsDatabaseType());
            m.Unit.ShouldBe(mvm.Unit.AsDatabaseType());
        }

        public static MedicineViewModel GetFakeMedicineViewModel()
        {
            var mvm = new MedicineViewModel
            {
                Amount = 10,
                Expired = false,
                Id = 1,
                MedType = MedType.Liquid,
                Unit = Unit.Mililiters,
                Name = "Xenna Extra Forte Plus",
                Refund = 0.95M,
                UnitPrice = 19.99M
            };
            return mvm;
        }
    }
}
