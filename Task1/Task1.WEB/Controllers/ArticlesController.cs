using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Task1.BL.DTO;
using Task1.BL.Services;

namespace Task1.WEB.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _articleService.GetPage(page, pageSize);
        var itemsCount = await _articleService.GetCount();

        var pageCount = (int)Math.Ceiling((double)itemsCount / pageSize);
        if (pageCount <= 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<ArticleDto>()
        {
            Items = result.ToList(),
            Page = page,
            PageCount = pageCount,
            PageSize = pageSize
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _articleService.GetById(id);

        return View(result);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleDto dto)
    {
        await _articleService.Add(dto);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteById(id);

        return RedirectToAction("Index");
    }
}