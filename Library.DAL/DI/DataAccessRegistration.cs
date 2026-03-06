using Library.DAL.Interfaces;
using Library.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DAL.DI
{
    public static class DataAccessRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthorsRepo, AuthorsRepo>();
            services.AddScoped<IBooksRepo, BooksRepo>();
            services.AddScoped<IBooksCategoriesRepo, BooksCategoriesRepo>();
            services.AddScoped<IBooksCopiesRepo, BooksCopiesRepo>();
            services.AddScoped<IBorrowingRepo, BorrowingRepo>();
            services.AddScoped<IFinesRepo, FinesRepo>();
            services.AddScoped<IUsersRepo, UsersRepo>();
            services.AddScoped<ISettingsRepo, SettingsRepo>();
            services.AddScoped<IRefreshTokensRepo, RefreshTokensRepo>();
            return services;
        }
    }
}
