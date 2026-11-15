using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wt_lab5_5razor_lisger.Models;
using wt_lab8_8razor_postgresql_liger.Data;
using wt_lab8_8razor_postgresql_liger.Models;

namespace wt_lab8_8razor_postgresql_liger.Pages.Courses;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Course Course { get; set; } = new();

    public SelectList TeachersList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        // Загружаем преподавателей из БД для выпадающего списка
        var teachers = await _context.Teachers.ToListAsync();
        TeachersList = new SelectList(teachers, nameof(Teacher.Id), nameof(Teacher.FullName));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Если ошибка, нужно снова заполнить список
            var teachers = await _context.Teachers.ToListAsync();
            TeachersList = new SelectList(teachers, nameof(Teacher.Id), nameof(Teacher.FullName));
            return Page();
        }

        _context.Courses.Add(Course);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}