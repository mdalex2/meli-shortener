using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meli.Models
{
    public class ConnectionDbContextClass : DbContext
    {
        public ConnectionDbContextClass(DbContextOptions<ConnectionDbContextClass> options) : base (options)
        {

        }
        public DbSet<UrlConfigClass> UrlConfigs { get; set; }
    }
}
