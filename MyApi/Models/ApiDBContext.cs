﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Models
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options)
            : base(options)
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Establishments> Establishments { get; set; }
    }
}
