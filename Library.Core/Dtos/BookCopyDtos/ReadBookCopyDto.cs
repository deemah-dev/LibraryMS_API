using Library.Core.Dtos.BookDtos;
using Library.Core.Models;

namespace Library.Core.Dtos.BookCopyDtos
{
    public class ReadBookCopyDto
    {
        public int CopyId { get; set; }
        public bool IsAvailable { get; set; }

        public ReadBookDto? Book { get; set; }
    }
}
