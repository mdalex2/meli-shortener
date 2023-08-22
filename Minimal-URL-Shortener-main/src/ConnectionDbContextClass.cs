using Microsoft.EntityFrameworkCore;

namespace UrlShortener
{
    public class ConnectionDbContextClass : DbContext
    {
        public ConnectionDbContextClass(DbContextOptions<ConnectionDbContextClass> options) : base(options)
        {

        }
        public DbSet<ShortUrl> ShortUrl { get; set; }
    }
}
