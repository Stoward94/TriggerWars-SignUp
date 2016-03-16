using System.Data.Entity;

namespace TriggerWars_SignUp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<SignUp> SignUps { get; set; }
    }

}