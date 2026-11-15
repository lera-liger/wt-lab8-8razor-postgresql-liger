using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab5_5razor_lisger.Models;
using wt_lab8_8razor_postgresql_liger.Data;
using wt_lab8_8razor_postgresql_liger.Models;

namespace wt_lab8_8razor_postgresql_liger.Pages.Teachers;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Teacher Teacher { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Teachers.Add(Teacher);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}