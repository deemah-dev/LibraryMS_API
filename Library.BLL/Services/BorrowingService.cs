using AutoMapper;
using Library.BLL.Interfaces;
using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;
using Library.DAL.Interfaces;

namespace Library.BLL.Services
{
    public class BorrowingService : IBorrowingService
    {
        private IBorrowingRepo borrowingRepo;
        private IBooksCopiesService copiesService;
        private IUsersService usersService;
        private IMapper mapper;

        public BorrowingService(IBorrowingRepo borrowingRepo, IMapper mapper, IBooksCopiesService copiesService, IUsersService usersService)
        {
            this.borrowingRepo = borrowingRepo;
            this.mapper = mapper;
            this.copiesService = copiesService;
            this.usersService = usersService;
        }

        public int BorrowBook(BorrowBookDto borrowBook)
        {
            BorrowingRecord borrowingRecordEntity = mapper.Map<BorrowingRecord>(borrowBook);
            return borrowingRepo.InsertBorrowingRecord(borrowingRecordEntity);
        }

        public bool ReturnBook(ReturnBookDto returnBook)
        {
            BorrowingRecord borrowingRecordEntity = mapper.Map<BorrowingRecord>(returnBook);
            return borrowingRepo.ReturnBook(borrowingRecordEntity);
        }

        public decimal HasLateFine(int borrowingRecordId, DateTime actualReturnDate)
        {
            return borrowingRepo.CheckBorrowFine(borrowingRecordId, actualReturnDate);
        }

        public IEnumerable<ReadBorrowingRecordDto>? GetAllBorrowingRecords()
        {
            return mapper.Map<IEnumerable<ReadBorrowingRecordDto>>(borrowingRepo.GetBorrowingRecords());
        }

        public ReadBorrowingRecordDto? GetBorrowingRecord(int borrowingRecordId)
        {
            BorrowingRecord? borrowingRecordEntity = borrowingRepo.GetBorrowingRecord(borrowingRecordId);
            if (borrowingRecordEntity is null)
                return null;

            ReadBorrowingRecordDto borrowingRecord = mapper.Map<ReadBorrowingRecordDto>(borrowingRecordEntity);
            borrowingRecord.Copy = copiesService.GetCopy(borrowingRecordEntity.CopyId);
            borrowingRecord.User = usersService.GetUser(borrowingRecordEntity.UserId);
            return borrowingRecord;
        }
    }
}
