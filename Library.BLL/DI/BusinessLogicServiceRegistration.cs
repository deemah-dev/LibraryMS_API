using Library.BLL.AutoMapper;
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
            services.AddScoped<IRefreshTokensService, RefreshTokensService>();

            services.AddAutoMappers();

            return services;
        }
        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(map => map.AddProfile(new BookAutoMapper()));
            services.AddAutoMapper(map => map.AddProfile(new BookCopyAutoMapper()));
            services.AddAutoMapper(map => map.AddProfile(new BorrowingAutoMapper()));
            return services;
        }
    }
}
