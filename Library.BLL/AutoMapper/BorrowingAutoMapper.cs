using AutoMapper;
using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;

namespace Library.BLL.AutoMapper
{
    public class BorrowingAutoMapper : Profile
    {
        public BorrowingAutoMapper()
        {
            CreateMap<BorrowingRecord, BorrowBookDto>().ReverseMap();
            CreateMap<BorrowingRecord, ReturnBookDto>().ReverseMap();
            CreateMap<BorrowingRecord, ReadBorrowingRecordDto>().ReverseMap();
        }
    }
}
