using DocHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data
{
    public class DocHubDbContext:DbContext
    {
        public DocHubDbContext(DbContextOptions<DocHubDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<DDocument> Documents { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
