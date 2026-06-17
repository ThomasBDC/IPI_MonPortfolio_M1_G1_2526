using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonPortfolio_M1_2526.Entities;

namespace MonPortfolio_M1_2526.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
