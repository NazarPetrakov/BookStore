using AutoMapper;
using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Publisher;
using BookStore.Domain.Models.Author;
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

            CreateMap<Author, CreateAuthor>().ReverseMap();
            CreateMap<Author, GetAuthor>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Author, UpdateAuthor>().ReverseMap();



        }
    }
}
