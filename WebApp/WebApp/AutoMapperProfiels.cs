using AutoMapper;
using Common.Utils;
using DAL;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.OrderModels;

namespace WebApp
{
    public static class AutoMapperProfiels
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Apothecary, ApothecaryViewModel>().ReverseMap();

                mapper.CreateMap<Medicine, MedicineViewModel>()
                    //.ForMember(m => m.MedicineType.Id, x => x.MapFrom(mvm => mvm.MedicineType.Id))
                    ;

                mapper.CreateMap<MedicineViewModel, Medicine>()
                    .ForMember(m => m.MedicineTypeId, x => x.MapFrom(mvm => mvm.MedicineType.Id))
                    ;
                mapper.CreateMap<MedicineTypeViewModel, MedicineType>()
                    .ForMember(mt => mt.MedType, x => x.MapFrom(mvtm => mvtm.MedType.AsDatabaseType()))
                    .ForMember(mt => mt.Unit, x => x.MapFrom(mtvm => mtvm.Unit.AsDatabaseType()));

                mapper.CreateMap<ApothecaryViewModel, ApothecarySelectViewModel>()
                    .ForMember(dest => dest.FullName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName} | ID - {src.Id}"));

                mapper.CreateMap<Order, OrderViewModel>()
                    .ForMember(dest => dest.ApothecaryFullName, x => x.MapFrom(src => src.Apothecary.FirstName + " " + src.Apothecary.LastName))
                    .ReverseMap();
                //.ForMember(dest => dest.Apothecary, x => x.MapFrom(src => src.Apothecary))

                mapper.CreateMap<OrderItem, OrderItemViewModel>()
                    .ForMember(dest => dest.Medicine, x => x.MapFrom(src => Mapper.Map<MedicineViewModel>(src.Medicine)));

                mapper.CreateMap<Order, OrderDetailViewModel>()
                    .IncludeBase<Order, OrderViewModel>()
                    .ForMember(dest => dest.Items, x => x.MapFrom(src => Mapper.Map<IEnumerable<OrderItemViewModel>>(src.OrderItems.AsEnumerable())));
            });
        }
    }
}
