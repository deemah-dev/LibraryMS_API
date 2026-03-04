using Library.Core.Models;

namespace Library.DAL.Interfaces
{
    public interface ISettingsRepo
    {
        Settings? GetSettings();
        bool UpdateDefaultBorrowDaysSettings(int defaultBorrowDays);
        bool UpdateDefaultFinePerDaySettings(decimal defaultFinePerDay);
    }
}
