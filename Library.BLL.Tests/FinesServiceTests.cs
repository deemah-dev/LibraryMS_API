using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class FinesServiceTests
    {
        [Fact]
        public void GetAllFines_WhenCalled_ReturnsListOfFines()
        {
            Mock<IFinesRepo> mockRepo = new();

            List<Fine> fines = new()
            {
                new Fine { FineId = 1, UserId = 1, BorrowingRecordId = 1, NumberOfLateDays = 3, FineAmount = 15 },
                new Fine { FineId = 2, UserId = 2, BorrowingRecordId = 2, NumberOfLateDays = 5, FineAmount = 25 },
                new Fine { FineId = 3, UserId = 1, BorrowingRecordId = 3, NumberOfLateDays = 7, FineAmount = 35 }
            };

            mockRepo.Setup(repo => repo.GetFines()).Returns(fines);

            FinesService service = new(mockRepo.Object);

            var result = service.GetAllFines();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(1, result.First().FineId);
            Assert.Equal(15, result.First().FineAmount);
            mockRepo.Verify(repo => repo.GetFines(), Times.Once);
        }

        [Fact]
        public void GetAllFines_WhenNoFinesExist_ReturnsNull()
        {
            Mock<IFinesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetFines()).Returns((IEnumerable<Fine>?)null);

            FinesService service = new(mockRepo.Object);

            var result = service.GetAllFines();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetFines(), Times.Once);
        }

        [Fact]
        public void GetAllFines_WhenEmptyListReturned_ReturnsEmptyEnumerable()
        {
            Mock<IFinesRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetFines()).Returns(new List<Fine>());

            FinesService service = new(mockRepo.Object);

            var result = service.GetAllFines();

            Assert.NotNull(result);
            Assert.Empty(result);
            mockRepo.Verify(repo => repo.GetFines(), Times.Once);
        }
    }
}
