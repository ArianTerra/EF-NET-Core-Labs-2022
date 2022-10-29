using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Task1.BL.DTO;
using Task1.BL.Services;

namespace Task1.WEB.Controllers;

public class TeachersController : Controller
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _teacherService.GetPage(page, pageSize);
        var itemsCount = await _teacherService.GetCount();

        var pageCount = (int)Math.Ceiling((double)itemsCount / pageSize);
        if (pageCount <= 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<TeacherDto>()
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
        var result = await _teacherService.GetById(id);

        return View(result);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(TeacherDto dto)
    {
        await _teacherService.Add(dto);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult>  Delete(Guid id)
    {
        await _teacherService.DeleteById(id);

        return RedirectToAction("Index");
    }
}