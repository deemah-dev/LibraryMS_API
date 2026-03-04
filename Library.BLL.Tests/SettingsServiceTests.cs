using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class SettingsServiceTests
    {
        //GetSettings
        [Fact]
        public void GetSettings_WhenCalled_ReturnsSettings()
        {
            Mock<ISettingsRepo> mockRepo = new();

            Settings settings = new Settings
            {
                Id = 1,
                DefaultBorrowDays = 14,
                DefaultFinePerDay = 5
            };

            mockRepo.Setup(repo => repo.GetSettings()).Returns(settings);

            SettingsService service = new(mockRepo.Object);

            var result = service.GetSettings();

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(14, result.DefaultBorrowDays);
            Assert.Equal(5, result.DefaultFinePerDay);
            mockRepo.Verify(repo => repo.GetSettings(), Times.Once);
        }

        [Fact]
        public void GetSettings_WhenSettingsDoNotExist_ReturnsNull()
        {
            Mock<ISettingsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetSettings()).Returns((Settings?)null);

            SettingsService service = new(mockRepo.Object);

            var result = service.GetSettings();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetSettings(), Times.Once);
        }


        //UpdateDefaultBorrowDays
        [Fact]
        public void UpdateDefaultBorrowDays_WhenCalled_ReturnsTrue()
        {
            Mock<ISettingsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.UpdateDefaultBorrowDaysSettings(21)).Returns(true);

            SettingsService service = new(mockRepo.Object);

            var result = service.UpdateDefaultBorrowDays(21);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateDefaultBorrowDaysSettings(21), Times.Once);
        }

        [Fact]
        public void UpdateDefaultBorrowDays_WhenFailed_ReturnsFalse()
        {
            Mock<ISettingsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.UpdateDefaultBorrowDaysSettings(999)).Returns(false);

            SettingsService service = new(mockRepo.Object);

            var result = service.UpdateDefaultBorrowDays(999);

            Assert.False(result);
            mockRepo.Verify(repo => repo.UpdateDefaultBorrowDaysSettings(999), Times.Once);
        }


        //UpdateDefaultFinePerDay
        [Fact]
        public void UpdateDefaultFinePerDay_WhenCalled_ReturnsTrue()
        {
            Mock<ISettingsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.UpdateDefaultFinePerDaySettings(10)).Returns(true);

            SettingsService service = new(mockRepo.Object);

            var result = service.UpdateDefaultFinePerDay(10);

            Assert.True(result);
            mockRepo.Verify(repo => repo.UpdateDefaultFinePerDaySettings(10), Times.Once);
        }

        [Fact]
        public void UpdateDefaultFinePerDay_WhenFailed_ReturnsFalse()
        {
            Mock<ISettingsRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.UpdateDefaultFinePerDaySettings(-5)).Returns(false);

            SettingsService service = new(mockRepo.Object);

            var result = service.UpdateDefaultFinePerDay(-5);

            Assert.False(result);
            mockRepo.Verify(repo => repo.UpdateDefaultFinePerDaySettings(-5), Times.Once);
        }
    }
}
