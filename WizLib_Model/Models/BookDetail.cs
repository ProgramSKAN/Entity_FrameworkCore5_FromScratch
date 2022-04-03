using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    //instead of Category table Busiess decided a change to have bookdetail table
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public double Weight { get; set; }

        //this enforces 1:1 relationship. ie; Book detail has 1 book. and book table has 1 detail for a book
        public Book Book { get; set; } //1:1
    }
}
