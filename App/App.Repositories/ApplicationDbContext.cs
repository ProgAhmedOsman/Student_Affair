using APP.Domain.Entities;
using APP.Domain.EntitiesBuilders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ClassRoomMap(modelBuilder.Entity<ClassRoom>());
            new SubjectMap(modelBuilder.Entity<Subject>());
            new StudentMap(modelBuilder.Entity<Student>());
            new StudentSubjectMap(modelBuilder.Entity<StudentSubject>());
        }
    }
}