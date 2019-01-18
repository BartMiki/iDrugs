using AutoMapper;
using Common.Utils;
using DAL;
using DAL.Enums;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.DoctorModels;
using WebApp.Models.DrugStoreStockModels;
using WebApp.Models.MedicineModels;
using WebApp.Models.OrderModels;
using WebApp.Models.PrescriptionModels;
using WebApp.Models.WarehouseModels;

namespace WebApp
{
    public static class AutoMapperProfiels
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(mapper =>
            {
                #region Apothecary Conversions
                mapper.CreateMap<Apothecary, ApothecaryViewModel>().ReverseMap();

                mapper.CreateMap<ApothecaryViewModel, ApothecarySelectViewModel>()
                    .ForMember(dest => dest.FullName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName} | ID: {src.Id}"));
                #endregion
                #region Medicine Conversions
                mapper.CreateMap<Medicine, MedicineViewModel>()
                    .ForMember(d => d.MedType, x => x.MapFrom(s => s.MedType.AsEnum<MedType>()))
                    .ForMember(d => d.Unit, x => x.MapFrom(s => s.Unit.AsEnum<Unit>()))
                    ;

                mapper.CreateMap<MedicineViewModel, MedicineSelectModel>()
                    .ForMember(d => d.FullName, x => x.MapFrom(s => $"{s.Name} | {s.MedicineContent}"))
                    ;

                mapper.CreateMap<MedicineViewModel, Medicine>()
                    .ForMember(d => d.MedType, x => x.MapFrom(s => s.MedType.AsDatabaseType()))
                    .ForMember(d => d.Unit, x => x.MapFrom(s => s.Unit.AsDatabaseType()))
                    ;

                mapper.CreateMap<MedicineDetailsViewModel, MedicineViewModel>().ReverseMap();

                mapper.CreateMap<CreateMedicineViewModel, Medicine>()
                    .ForMember(d => d.MedType, x => x.MapFrom(s => s.MedType.AsDatabaseType()))
                    .ForMember(d => d.Unit, x => x.MapFrom(s => s.Unit.AsDatabaseType()))
                    ;
                #endregion
                #region Order Conversions
                mapper.CreateMap<AddOrderItemViewModel, OrderItem>();

                mapper.CreateMap<OrderItem, EditOrderItemViewModel>()
                    .ForMember(d => d.MedicineName, x => x.MapFrom(s => s.Medicine.Name))
                    .ReverseMap();


                mapper.CreateMap<Order, OrderViewModel>()
                    .ForMember(dest => dest.ApothecaryFullName, x => x.MapFrom(src => src.Apothecary.FirstName + " " + src.Apothecary.LastName))
                    .ReverseMap();

                mapper.CreateMap<OrderItem, OrderItemViewModel>()
                    .ForMember(dest => dest.Medicine, x => x.MapFrom(src => Mapper.Map<MedicineViewModel>(src.Medicine)))
                    .ForMember(dest => dest.OrderId, x => x.MapFrom(src => src.OrderId))
                    ;

                mapper.CreateMap<Order, OrderDetailViewModel>()
                    .IncludeBase<Order, OrderViewModel>()
                    .ForMember(dest => dest.Items, x => x.MapFrom(src => Mapper.Map<IEnumerable<OrderItemViewModel>>(src.OrderItems.AsEnumerable())));
                #endregion
                #region Warehouse Conversions
                mapper.CreateMap<MedicineWarehouse, WarehouseItemViewModel>().ReverseMap();
                //.ForMember(d => d.Medicine, x => x.MapFrom(s => s.Medicine));

                mapper.CreateMap<WarehouseItemViewModel, AddWarehouseItemViewModel>().ReverseMap();
                #endregion
                #region DrugStore Stock Conversions
                mapper.CreateMap<DrugStoreStockViewModel, DrugStoreAvailableMedicine>().ReverseMap();
                #endregion
                #region Doctor Conversions
                mapper.CreateMap<Doctor, DoctorViewModel>().ReverseMap();

                mapper.CreateMap<DoctorViewModel, DoctorSelectViewModel>()
                    .ForMember(dest => dest.FullName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName} | ID: {src.Id}"));

                mapper.CreateMap<Doctor, DoctorSelectViewModel>()
                    .ForMember(dest => dest.FullName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName} | ID: {src.Id}"));
                #endregion
                #region Prescription Conversions

                mapper.CreateMap<PrescriptionItem, PrescriptionItemViewModel>()
                    .ForMember(dest => dest.Status, x => x.MapFrom(src => src.Status.AsEnum<PrescriptionItemStatusEnum>()));

                mapper.CreateMap<PrescriptionItemViewModel, PrescriptionItem>()
                    .ForMember(dest => dest.Status, x => x.MapFrom(src => src.Status.AsDatabaseType()));

                mapper.CreateMap<Prescription, PrescriptionViewModel>()
                    .ForMember(dest => dest.ApothecaryFullName, x => x.MapFrom(src => $"{src.Apothecary.FirstName} {src.Apothecary.LastName}"))
                    .ForMember(dest => dest.DoctorFullName, x => x.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
                    .ForMember(dest => dest.Status, x => x.MapFrom(src => src.Status.AsEnum<PrescriptionStatusEnum>()));
                
                mapper.CreateMap<PrescriptionViewModel, Prescription>()
                    .ForMember(dest => dest.Status, x => x.MapFrom(src => src.Status.AsDatabaseType()));

                mapper.CreateMap<Prescription, CreatePrescriptionViewModel>()
                    .IncludeBase<Prescription, PrescriptionViewModel>()
                    .ForMember(dest => dest.ApothecarySelect, map => map.Ignore())
                    .ForMember(dest => dest.DoctorSelect, map => map.Ignore())
                    .ReverseMap();

                #endregion
            });
        }
    }
}
