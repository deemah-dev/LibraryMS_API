using Library.BLL.Services;
using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;
using Moq;
using AutoMapper;
using Library.BLL.Interfaces;

namespace Library.BLL.Tests
{
    public class BorrowingServiceTests
    {
        //BorrowBook
        [Fact]
        public void BorrowBook_WhenCalled_ReturnsBorrowingRecordId()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            BorrowBookDto borrowDto = new BorrowBookDto
            {
                UserId = 1,
                CopyId = 1,
                BorrowingDate = DateTime.Now
            };

            BorrowingRecord mapped = new BorrowingRecord
            {
                BorrowUserId = 1,
                CopyId = 1,
                BorrowingDate = borrowDto.BorrowingDate,
                DueDate = DateTime.Now.AddDays(14)
            };

            mockMapper.Setup(m => m.Map<BorrowingRecord>(It.IsAny<BorrowBookDto>())).Returns(mapped);
            mockCopies.Setup(c => c.GetCopy(mapped.CopyId)).Returns(new ReadBookCopyDto { CopyId = 1, IsAvailable = true });
            mockRepo.Setup(r => r.InsertBorrowingRecord(mapped)).Returns(1);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.BorrowBook(borrowDto);

            Assert.Equal(1, result);
            mockRepo.Verify(repo => repo.InsertBorrowingRecord(mapped), Times.Once);
        }


        //ReturnBook
        [Fact]
        public void ReturnBook_WhenCalled_ReturnsTrue()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            var returnDto = new ReturnBookDto { BorrowingRecordId = 1, UserId = 1, ActualReturnDate = DateTime.Now };
            var mapped = new BorrowingRecord { BorrowingRecordId = 1, BorrowUserId = 1, ActualReturnDate = returnDto.ActualReturnDate };

            mockMapper.Setup(m => m.Map<BorrowingRecord>(It.IsAny<ReturnBookDto>())).Returns(mapped);
            mockRepo.Setup(r => r.ReturnBook(mapped)).Returns(true);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.ReturnBook(returnDto);

            Assert.True(result);
            mockRepo.Verify(repo => repo.ReturnBook(mapped), Times.Once);
        }

        [Fact]
        public void ReturnBook_WhenFailed_ReturnsFalse()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            var returnDto = new ReturnBookDto { BorrowingRecordId = 999, UserId = 1, ActualReturnDate = DateTime.Now };
            var mapped = new BorrowingRecord { BorrowingRecordId = 999, BorrowUserId = 1, ActualReturnDate = returnDto.ActualReturnDate };

            mockMapper.Setup(m => m.Map<BorrowingRecord>(It.IsAny<ReturnBookDto>())).Returns(mapped);
            mockRepo.Setup(r => r.ReturnBook(mapped)).Returns(false);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.ReturnBook(returnDto);

            Assert.False(result);
            mockRepo.Verify(repo => repo.ReturnBook(mapped), Times.Once);
        }


        //HasLateFine
        [Fact]
        public void HasLateFine_WhenFineExists_ReturnsFine()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            DateTime actualReturnDate = DateTime.Now.AddDays(5);
            mockRepo.Setup(repo => repo.CheckBorrowFine(1, actualReturnDate)).Returns(25m);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.HasLateFine(1, actualReturnDate);

            Assert.Equal(25m, result);
            mockRepo.Verify(repo => repo.CheckBorrowFine(1, actualReturnDate), Times.Once);
        }

        [Fact]
        public void HasLateFine_WhenNoFine_ReturnsNegativeOne()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            DateTime actualReturnDate = DateTime.Now;
            mockRepo.Setup(repo => repo.CheckBorrowFine(1, actualReturnDate)).Returns(-1m);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.HasLateFine(1, actualReturnDate);

            Assert.Equal(-1m, result);
            mockRepo.Verify(repo => repo.CheckBorrowFine(1, actualReturnDate), Times.Once);
        }


        //GetAllBorrowingRecords
        [Fact]
        public void GetAllBorrowingRecords_WhenCalled_ReturnsListOfRecords()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            List<BorrowingRecord> records = new()
            {
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 1, 
                    BorrowUserId = 1, 
                    CopyId = 1, 
                    BorrowingDate = DateTime.Now.AddDays(-7),
                    DueDate = DateTime.Now.AddDays(7)
                },
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 2, 
                    BorrowUserId = 2, 
                    CopyId = 2, 
                    BorrowingDate = DateTime.Now.AddDays(-14),
                    DueDate = DateTime.Now
                },
                new BorrowingRecord 
                { 
                    BorrowingRecordId = 3, 
                    BorrowUserId = 1, 
                    CopyId = 3, 
                    BorrowingDate = DateTime.Now.AddDays(-21),
                    DueDate = DateTime.Now.AddDays(-7),
                    ActualReturnDate = DateTime.Now.AddDays(-5)
                }
            };

            var dtoList = new List<Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto>
            {
                new Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto { BorrowingRecordId = 1 },
                new Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto { BorrowingRecordId = 2 },
                new Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto { BorrowingRecordId = 3 }
            };

            mockRepo.Setup(repo => repo.GetBorrowingRecords()).Returns(records);
            mockMapper.Setup(m => m.Map<IEnumerable<Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto>>(It.IsAny<IEnumerable<BorrowingRecord>>())).Returns(dtoList);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.GetAllBorrowingRecords();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(1, result.First().BorrowingRecordId);
            mockRepo.Verify(repo => repo.GetBorrowingRecords(), Times.Once);
        }

        [Fact]
        public void GetAllBorrowingRecords_WhenNoRecordsExist_ReturnsNull()
        {
            var mockRepo = new Mock<IBorrowingRepo>();
            var mockMapper = new Mock<IMapper>();
            var mockCopies = new Mock<IBooksCopiesService>();
            var mockUsers = new Mock<IUsersService>();

            mockRepo.Setup(repo => repo.GetBorrowingRecords()).Returns((IEnumerable<BorrowingRecord>?)null);
            mockMapper.Setup(m => m.Map<IEnumerable<Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto>>(It.IsAny<IEnumerable<BorrowingRecord>>())).Returns((IEnumerable<Core.Dtos.BorrowingBookDtos.ReadBorrowingRecordDto>?)null);

            BorrowingService service = new(mockRepo.Object, mockMapper.Object, mockCopies.Object, mockUsers.Object);

            var result = service.GetAllBorrowingRecords();

            Assert.Null(result);
            mockRepo.Verify(repo => repo.GetBorrowingRecords(), Times.Once);
        }
    }
}