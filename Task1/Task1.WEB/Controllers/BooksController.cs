using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Task1.BL.DTO;
using Task1.BL.Services;

namespace Task1.WEB.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _bookService.GetPage(page, pageSize);
        var itemsCount = await _bookService.GetCount();

        var pageCount = (int)Math.Ceiling((double)itemsCount / pageSize);
        if (pageCount <= 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<BookDto>()
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
        var result = await _bookService.GetById(id);

        return View(result);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookDto dto)
    {
        await _bookService.Add(dto);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _bookService.DeleteById(id);

        return RedirectToAction("Index");
    }
}