using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_liger.Data;
using wt_lab8_8razor_postgresql_liger.Models;

namespace wt_lab8_8razor_postgresql_liger.Pages.Students;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    // Свойство для поиска (поддерживает GET-запрос)
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<Student> Students { get; set; } = new();

    public async Task OnGetAsync()
    {
        // Базовый запрос к БД
        var query = _context.Students.AsQueryable();

        // Если введен поиск — фильтруем по ФИО или специальности
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(s =>
                s.FullName.Contains(Search) ||
                s.Specialty.Contains(Search));
        }

        // Сортировка и выполнение запроса
        Students = await query
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    // Обработчик удаления (если используется на странице)
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}