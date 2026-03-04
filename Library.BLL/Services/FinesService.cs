using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class FinesService : IFinesService
    {
        private IFinesRepo finesRepo;

        public FinesService(IFinesRepo finesRepo)
        {
            this.finesRepo = finesRepo;
        }

        public IEnumerable<Fine>? GetAllFines()
        {
            return finesRepo.GetFines();
        }
    }
}
