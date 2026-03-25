using AutoMapper;
using Library.Core.Dtos.BookDtos;
using Library.Core.Models;

namespace Library.BLL.AutoMapper
{
    public class BookAutoMapper : Profile
    {
        public BookAutoMapper()
        {
            CreateMap<Book, AddBookDTO>().ReverseMap();
            CreateMap<Book, ReadBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
        }
    }
}
