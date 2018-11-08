using AutoMapper;
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
            });
        }
    }
}
