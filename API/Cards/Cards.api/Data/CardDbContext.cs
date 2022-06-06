using Cards.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.api.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions options) : base(options)
        {
        }

        //Dbset
        public DbSet<Card> Cards { get; set; }
    }
}
