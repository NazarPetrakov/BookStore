using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Repositories;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Services;
using BookStore.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
