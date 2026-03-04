using Library.Core.Models;

namespace Library.BLL.Interfaces
{
    public interface ISettingsService
    {
        Settings? GetSettings();
        bool UpdateDefaultBorrowDays(int defaultBorrowDays);
        bool UpdateDefaultFinePerDay(decimal defaultFinePerDay);
    }
}
