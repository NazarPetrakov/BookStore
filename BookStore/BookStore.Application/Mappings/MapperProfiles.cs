using AutoMapper;
using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.Category;
using BookStore.Application.Contracts.Publisher;
using BookStore.Application.Contracts.Review;
using BookStore.Application.Contracts.User;
using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;
using BookStore.Domain.Models.Publisher;
using BookStore.Domain.Models.Review;
using BookStore.Domain.Models.User;

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
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.PublicationYear)))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category)));
            CreateMap<UpdateBook, Book>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.ToDateTime(TimeOnly.MinValue))); ;

            CreateMap<ApplicationUser, GetUser>().ReverseMap();

            CreateMap<CreateReview, Review>().ReverseMap();
            CreateMap<Review, GetReview>().ReverseMap();
        }
    }
}
