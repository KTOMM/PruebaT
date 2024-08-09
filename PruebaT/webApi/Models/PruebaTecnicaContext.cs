using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace webApi.Models
{
    public partial class PruebaTecnicaContext : DbContext
    {
        public PruebaTecnicaContext()
        {
        }

        public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
            : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

           
        }

      
    }
}
