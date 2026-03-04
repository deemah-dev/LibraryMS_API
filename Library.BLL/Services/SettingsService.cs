using Library.BLL.Interfaces;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettingsRepo settingsRepo;

        public SettingsService(ISettingsRepo settingsRepo)
        {
            this.settingsRepo = settingsRepo;
        }

        public Settings? GetSettings()
        {
            return settingsRepo.GetSettings();
        }

        public bool UpdateDefaultBorrowDays(int defaultBorrowDays)
        {
            return settingsRepo.UpdateDefaultBorrowDaysSettings(defaultBorrowDays);
        }

        public bool UpdateDefaultFinePerDay(decimal defaultFinePerDay)
        {
            return settingsRepo.UpdateDefaultFinePerDaySettings(defaultFinePerDay);
        }
    }
}
