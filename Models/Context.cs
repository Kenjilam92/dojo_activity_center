using Microsoft.EntityFrameworkCore;

namespace dojo_activity.Models
{
    public class Context : DbContext
    {
        public Context (DbContextOptions options) : base (options) {}
        public DbSet<User> Users {get;set;}
        public DbSet<Act>   Acts {get;set;}
        public DbSet<Associate> Associates{get;set;}
    }
}