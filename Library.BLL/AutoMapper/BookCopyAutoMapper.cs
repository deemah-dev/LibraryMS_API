using AutoMapper;
using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Models;

namespace Library.BLL.AutoMapper
{
    public class BookCopyAutoMapper : Profile
    {
        public BookCopyAutoMapper()
        {
            CreateMap<BookCopy, AddBookCopyDto>().ReverseMap();
            CreateMap<BookCopy, ReadBookCopyDto>().ReverseMap();
        }
    }
}
