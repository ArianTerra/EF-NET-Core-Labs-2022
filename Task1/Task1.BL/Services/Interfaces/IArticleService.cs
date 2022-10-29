using Task1.BL.DTO;

namespace Task1.BL.Services;

public interface IArticleService
{
    Task<ArticleDto> GetById(Guid id);
    Task<IEnumerable<ArticleDto>> GetPage(int pageNumber, int pageSize);
    Task<int> GetCount();
    Task Add(ArticleDto dto);
    Task Edit(ArticleDto dto);
    Task DeleteById(Guid id);
}