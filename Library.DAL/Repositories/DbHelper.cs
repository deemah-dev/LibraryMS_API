using Microsoft.Extensions.Configuration;
namespace Library.DAL.Repositories
{
    internal static class DbHelper
    {
        //private static readonly IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //public static string ConnectionString => _configuration.GetSection("DefaultConnection").Value.ToString();
        public static string ConnectionString = "Server=.; Database=LibraryDB; Integrated Security=SSPI; TrustServerCertificate=True;";
    }
}
