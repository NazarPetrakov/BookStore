using AutoMapper;
using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.Category;
using BookStore.Application.Contracts.Publisher;
using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;
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

            CreateMap<Category, CreateCategory>().ReverseMap();
            CreateMap<Category, GetCategory>().ReverseMap();
            CreateMap<Category, UpdateCategory>().ReverseMap();

            CreateMap<CreateBook, Book>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.ToDateTime(TimeOnly.MinValue)));
            CreateMap<Book, GetBook>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.PublicationYear)));
            CreateMap<UpdateBook, Book>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.ToDateTime(TimeOnly.MinValue))); ;

        }
    }
}
