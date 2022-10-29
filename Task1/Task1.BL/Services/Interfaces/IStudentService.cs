using Task1.BL.DTO;

namespace Task1.BL.Services;

public interface IStudentService
{
    Task<StudentDto> GetById(Guid id);
    Task<IEnumerable<StudentDto>> GetPage(int pageNumber, int pageSize);
    Task<int> GetCount();
    Task Add(StudentDto dto);
    Task Edit(StudentDto dto);
    Task DeleteById(Guid id);
}