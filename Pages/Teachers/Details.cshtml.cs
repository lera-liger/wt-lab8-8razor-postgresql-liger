using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab5_5razor_lisger.Models;
using wt_lab8_8razor_postgresql_liger.Data;
using wt_lab8_8razor_postgresql_liger.Models;

namespace wt_lab8_8razor_postgresql_liger.Pages.Teachers;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Teacher? Teacher { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);

        if (Teacher == null)
        {
            return NotFound();
        }

        return Page();
    }

    // Обработчик удаления
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}