using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public partial class AppDbContext: DbContext
    {

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

    #region .ctors
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
#endregion
    }
}
