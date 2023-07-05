using ForestrySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForestrySystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<ForestryInstitution> Institutions { get; set; }
        public DbSet<TypeOfWood> WoodTypes { get; set; }
        public DbSet<ForestrySystem.Models.PurposeOfCutOff> PurposeOfCutOff { get; set; }
        public DbSet<ForestrySystem.Models.TypeOfTimber> TypeOfTimber { get; set; }
        public DbSet<ForestrySystem.Models.Events> Events { get; set; }
        public DbSet<ForestrySystem.Models.CategoryOfTimber> CategoryOfTimber { get; set; }
        public DbSet<AppUser> Users { get; set; }
    }
}
