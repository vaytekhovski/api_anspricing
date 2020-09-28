using API_ANSPRICING.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ANSPRICING
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=157.230.97.233;Port=6606;Database=ANSPRICING;Uid=pro;Pwd=rsE>9S^2Fu:kNVc:;charset=utf8");
        }
        
        public DbSet<Station> stations { get; set; }
        public DbSet<Tag> tags { get; set; }
    }
}
