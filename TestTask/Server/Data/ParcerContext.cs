using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TestTask.Shared.ModelsDb;

namespace TestTask.Server.Data
{
    public class ParcerContext : DbContext
    {
        public ParcerContext(DbContextOptions<ParcerContext> options) : base(options) => Database.EnsureCreated();

        public DbSet<Link> Links { get; set; }
        public DbSet<ParcedLink> ParcedLinks { get; set; }


    }
}

