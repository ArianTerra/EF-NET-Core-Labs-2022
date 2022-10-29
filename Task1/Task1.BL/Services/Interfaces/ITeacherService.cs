using Task1.BL.DTO;

namespace Task1.BL.Services;

public interface ITeacherService
{
    Task<TeacherDto> GetById(Guid id);
    Task<IEnumerable<TeacherDto>> GetPage(int pageNumber, int pageSize);
    Task<int> GetCount();
    Task Add(TeacherDto dto);
    Task Edit(TeacherDto dto);
    Task DeleteById(Guid id);
}