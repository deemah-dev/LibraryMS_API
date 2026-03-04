using Library.BLL.Services;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;

namespace Library.BLL.Tests
{
    public class BorrowingServiceTests
    {
        //BorrowBook
        [Fact]
        public void BorrowBook_WhenCalled_ReturnsBorrowingRecordId()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            BorrowingRecord borrowingRecord = new BorrowingRecord
            {
                UserId = 1,
                CopyId = 1,
                BorrowingDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            mockRepo.Setup(repo => repo.InsertBorrowingRecord(borrowingRecord)).Returns(1);

            BorrowingService service = new(mockRepo.Object);

            var result = service.BorrowBook(borrowingRecord);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBorrowingRecord(borrowingRecord), Times.Once);
        }


        //ReturnBook
        [Fact]
        public void ReturnBook_WhenCalled_ReturnsTrue()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            DateTime actualReturnDate = DateTime.Now;
            mockRepo.Setup(repo => repo.ReturnBook(1, 1, actualReturnDate)).Returns(true);

            BorrowingService service = new(mockRepo.Object);

            var result = service.ReturnBook(1, 1, actualReturnDate);

            Assert.True(result);
            mockRepo.Verify(repo => repo.ReturnBook(1, 1, actualReturnDate), Times.Once);
        }

        [Fact]
        public void ReturnBook_WhenFailed_ReturnsFalse()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            DateTime actualReturnDate = DateTime.Now;
            mockRepo.Setup(repo => repo.ReturnBook(999, 1, actualReturnDate)).Returns(false);

            BorrowingService service = new(mockRepo.Object);

            var result = service.ReturnBook(999, 1, actualReturnDate);

            Assert.False(result);
            mockRepo.Verify(repo => repo.ReturnBook(999, 1, actualReturnDate), Times.Once);
        }


        //HasLateFine
        [Fact]
        public void HasLateFine_WhenFineExists_ReturnsFine()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            DateTime actualReturnDate = DateTime.Now.AddDays(5);
            mockRepo.Setup(repo => repo.CheckBorrowFine(1, actualReturnDate)).Returns(25);

            BorrowingService service = new(mockRepo.Object);

            var result = service.HasLateFine(1, actualReturnDate);

            Assert.Equal(25, result);
            mockRepo.Verify(repo => repo.CheckBorrowFine(1, actualReturnDate), Times.Once);
        }

        [Fact]
        public void HasLateFine_WhenNoFine_ReturnsNegativeOne()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            DateTime actualReturnDate = DateTime.Now;
            mockRepo.Setup(repo => repo.CheckBorrowFine(1, actualReturnDate)).Returns(-1);

            BorrowingService service = new(mockRepo.Object);

            var result = service.HasLateFine(1, actualReturnDate);

            Assert.Equal(-1, result);
            mockRepo.Verify(repo => repo.CheckBorrowFine(1, actualReturnDate), Times.Once);
        }


        //GetAllBorrowingRecords
        [Fact]
        public void GetAllBorrowingRecords_WhenCalled_ReturnsListOfRecords()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            List<BorrowingRecord> records = new()
            {
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 1, 
                    UserId = 1, 
                    CopyId = 1, 
                    BorrowingDate = DateTime.Now.AddDays(-7),
                    DueDate = DateTime.Now.AddDays(7)
                },
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 2, 
                    UserId = 2, 
                    CopyId = 2, 
                    BorrowingDate = DateTime.Now.AddDays(-14),
                    DueDate = DateTime.Now
                },
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 3, 
                    UserId = 1, 
                    CopyId = 3, 
                    BorrowingDate = DateTime.Now.AddDays(-21),
                    DueDate = DateTime.Now.AddDays(-7),
                    ActualReturnDate = DateTime.Now.AddDays(-5)
                }
            };

            mockRepo.Setup(repo => repo.GetBorrowingRecords()).Returns(records);

            BorrowingService service = new(mockRepo.Object);

            var result = service.GetAllBorrowingRecords();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(1, result.First().BorrowingRecordId);
            mockRepo.Verify(repo => repo.GetBorrowingRecords(), Times.Once);
        }

        [Fact]
        public void GetAllBorrowingRecords_WhenNoRecordsExist_ReturnsNull()
        {
            Mock<IBorrowingRepo> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBorrowingRecords()).Returns((IEnumerable<BorrowingRecord>?)null);

            BorrowingService service = new(mockRepo.Object);

            var result = service.GetAllBorrowingRecords();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBorrowingRecords(), Times.Once);
        }
    }
}