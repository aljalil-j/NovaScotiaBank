using NovaScotia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Data
{
    public class ScotiaTransactionContext : DbContext
    {
        public ScotiaTransactionContext(DbContextOptions<ScotiaTransactionContext> options)
           : base(options)
        {
        }

        public DbSet<ScotiaTransaction> ScotiaTransaction { get; set; }
    }
}
