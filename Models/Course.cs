using System.ComponentModel.DataAnnotations;

namespace wt_lab8_8razor_postgresql_liger.Models;

public class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название курса")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Название курса")]
    public string Title { get; set; } = string.Empty;

    [Range(1, 4, ErrorMessage = "Семестр от 1 до 4")]
    [Display(Name = "Семестр")]
    public int Semester { get; set; }

    [Range(1, 300, ErrorMessage = "Количество часов от 1 до 300")]
    [Display(Name = "Часы")]
    public int Hours { get; set; }

    [Display(Name = "Преподаватель")]
    public int TeacherId { get; set; }   
}
