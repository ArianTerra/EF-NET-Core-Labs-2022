using EducationPortal.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Task1.BL.DTO;
using Task1.BL.Services;

namespace Task1.WEB.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var result = await _studentService.GetPage(page, pageSize);
        var itemsCount = await _studentService.GetCount();

        var pageCount = (int)Math.Ceiling((double)itemsCount / pageSize);
        if (pageCount <= 0)
        {
            pageCount = 1;
        }

        var viewModel = new PageViewModel<StudentDto>()
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
        var result = await _studentService.GetById(id);

        return View(result);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(StudentDto dto)
    {
        await _studentService.Add(dto);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _studentService.DeleteById(id);

        return RedirectToAction("Index");
    }
}