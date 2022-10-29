using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task1.BL.DTO;
using Task1.DAL.DomainModels.TPH;
using Task1.DAL.Repositories;

namespace Task1.BL.Services;

public class ArticleService : IArticleService
{
    private readonly IGenericRepository<Article> _repository;

    private readonly IMapper _mapper;

    public ArticleService(IGenericRepository<Article> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ArticleDto> GetById(Guid id)
    {
        var result = await _repository.FindFirstAsync(x => x.Id == id);

        if (result == null)
        {
            throw new ArgumentException("Not found");
        }

        var mapped = _mapper.Map<ArticleDto>(result);

        return mapped;
    }

    public async Task<IEnumerable<ArticleDto>> GetPage(int pageNumber, int pageSize)
    {
        int itemsCount = await _repository.CountAsync();
        int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);

        if (pagesCount == 0)
        {
            pagesCount = 1;
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size should be bigger than 0");
        }

        if (pageNumber <= 0 || pageNumber > pagesCount)
        {
            throw new ArgumentException("Page does not exist");
        }

        var items = await _repository.FindAll(
            page: pageNumber,
            pageSize: pageSize
        ).ToListAsync();

        var mapped = _mapper.Map<List<Article>, IEnumerable<ArticleDto>>(items);

        return mapped;
    }

    public async Task<int> GetCount()
    {
        return await _repository.CountAsync();
    }

    public async Task Add(ArticleDto dto)
    {
        var mapped = _mapper.Map<Article>(dto);

        await _repository.AddAsync(mapped);
    }

    public async Task Edit(ArticleDto dto)
    {
        var mapped = _mapper.Map<Article>(dto);

        await _repository.UpdateAsync(mapped);
    }

    public async Task DeleteById(Guid id)
    {
        var result = await _repository.FindFirstAsync(x => x.Id == id);

        if (result == null)
        {
            throw new ArgumentException("Not found");
        }

        await _repository.RemoveAsync(result);
    }
}