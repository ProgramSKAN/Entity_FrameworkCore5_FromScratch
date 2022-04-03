using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context):base(context)
        {

        }

        //Register All table models that needs to be created here
        public DbSet<Category> Categories { get; set; } // Table name: Categories with schema Category model
        public DbSet<Genre> Genres { get; set; } //if here is not added, then add-migration created empty migration. so you can delete that file and rerun
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }


    }
}
