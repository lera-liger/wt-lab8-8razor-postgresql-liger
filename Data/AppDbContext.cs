using Microsoft.EntityFrameworkCore;
using wt_lab5_5razor_lisger.Models;
using wt_lab8_8razor_postgresql_liger.Models;

namespace wt_lab8_8razor_postgresql_liger.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Начальные данные (Seed Data)
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Лигер Валерия Валерьевна", Specialty = "УИР", Course = 4, AverageGrade = 9.7, IsEnrolled = true },
            new Student { Id = 2, FullName = "Ясковец Александра Дмитривена", Specialty = "УИР", Course = 4, AverageGrade = 10, IsEnrolled = true }
        );

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, FullName = "Шаститко Дмитрий Витальевич", Department = "Кафедра УИР", ExperienceYears = 15, HasDegree = false },
            new Teacher { Id = 2, FullName = "Чаган Никита Федорович", Department = "Кафедра УИР", ExperienceYears = 5, HasDegree = false }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Web-Технологии", Semester = 7, Hours = 120, TeacherId = 1 },
            new Course { Id = 2, Title = "Гибкие методологии", Semester = 4, Hours = 100, TeacherId = 1 }
        );
    }
}
