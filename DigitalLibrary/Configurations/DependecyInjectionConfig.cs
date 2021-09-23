using DigitalLibrary.Data;
using DigitalLibrary.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalLibrary.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection AddDenpendencyConfig(this IServiceCollection services)
        {            

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookGenreRepository, BookGenreRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReserveRepository, ReserveRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
