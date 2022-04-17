using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_DataAccess.FluentConfig;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context):base(context)
        {

        }

        //Register All table models that needs to be created here
        //public DbSet<Category> Categories { get; set; } // Table name: Categories with schema Category model

        public DbSet<Genre> Genres { get; set; } //if here is not added, then add-migration created empty migration. so you can delete that file and rerun
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }


        //instead of Category table Busiess decided a change to have book details table
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }


        //Fluent API
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }

        //view
        public DbSet<BookDetailsFromView> BookDetailsFromViews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure fluent API

            //composite key  (many:many relation BookAuthor table)
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });


            //Fluent API demo (alternative to data annotations)
            //Book Details
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(b => b.BookDetail_Id);
            modelBuilder.Entity<Fluent_BookDetail>().Property(b=>b.NumberOfChapters).IsRequired();

            //modelBuilder.Entity<Fluent_Book>().HasOne().......  //OR
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailsConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());

            //combination of fluent api and data annotation
            //Change Category Table name and column name using fluent api and set [key] in model like data annotation 
            modelBuilder.Entity<Category>().ToTable("tbl_Category");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("CataegoryName");

            //View
            //create empty migration and write view sql query in up method and map return type here
            //HasNoKey() > tells EF core that there is no primary key. EF core never tracks this entity since it has no PK. so anything read from view is readonly.
            //eventhough if you set tracking explicitly EF core ignores it

            //ToView() > without this EF core creates BookDetailsFromView table in DB. so mention view map
            modelBuilder.Entity<BookDetailsFromView>().HasNoKey().ToView("GetOnlyBookDetails");
        }
    }
}
