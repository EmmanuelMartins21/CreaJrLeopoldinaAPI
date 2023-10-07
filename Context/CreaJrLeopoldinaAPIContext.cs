using CreaJrLeopoldinaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreaJrLeopoldinaAPI.Context
{
    public class CreaJrLeopoldinaAPIContext : DbContext
    {
        public CreaJrLeopoldinaAPIContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
    }
}
