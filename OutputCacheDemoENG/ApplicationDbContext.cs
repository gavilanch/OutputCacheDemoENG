﻿using Microsoft.EntityFrameworkCore;
using OutputCacheDemoESP.Entidades;

namespace OutputCacheDemoESP
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
