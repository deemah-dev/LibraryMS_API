using Library.BLL.Interfaces;
using Library.BLL.Services;
using Library.DAL.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BLL.DI
{
    public static class BusinessLogicServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddDataLayer();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IBorrowingService, BorrowingService>();
            services.AddScoped<IBooksCategoriesService, BooksCategoriesService>();
            services.AddScoped<IBooksCopiesService, BooksCopiesService>();
            services.AddScoped<IFinesService, FinesService>();
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<ISettingsService, SettingsService>();
            return services;
        }
    }
}
