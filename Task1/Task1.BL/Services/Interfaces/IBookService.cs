using Task1.BL.DTO;

namespace Task1.BL.Services;

public interface IBookService
{
    Task<BookDto> GetById(Guid id);
    Task<IEnumerable<BookDto>> GetPage(int pageNumber, int pageSize);
    Task<int> GetCount();
    Task Add(BookDto dto);
    Task Edit(BookDto dto);
    Task DeleteById(Guid id);
}