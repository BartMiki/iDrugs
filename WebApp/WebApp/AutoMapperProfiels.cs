using AutoMapper;
using Common.Utils;
using DAL;
using WebApp.Models;

namespace WebApp
{
    public static class AutoMapperProfiels
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Apothecary, ApothecaryViewModel>().ReverseMap();
                mapper.CreateMap<MedicineViewModel, Medicine>()
                    .ForMember(m => m.MedicineTypeId, x => x.MapFrom(mvm => mvm.MedicineType.Id))
                    ;
                mapper.CreateMap<MedicineTypeViewModel, MedicineType>()
                    .ForMember(mt => mt.MedType, x => x.MapFrom(mvtm => mvtm.MedType.AsDatabaseType()))
                    .ForMember(mt => mt.Unit, x => x.MapFrom(mtvm => mtvm.Unit.AsDatabaseType()));
            });
        }
    }
}
