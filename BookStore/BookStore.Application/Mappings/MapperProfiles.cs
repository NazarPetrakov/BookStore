using AutoMapper;
using BookStore.Application.Contracts.Publisher;
using BookStore.Domain.Models.Publisher;

namespace BookStore.Application.Mappings
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Publisher, CreatePublisher>().ReverseMap();
            CreateMap<Publisher, GetPublisher>().ReverseMap();
            CreateMap<Publisher, UpdatePublisher>().ReverseMap();



        }
    }
}
