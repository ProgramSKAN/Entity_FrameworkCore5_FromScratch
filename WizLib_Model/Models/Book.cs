using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    //without primary key migrations throws error. Book need primary key
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        [Required] //not null column
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }

        [NotMapped] //this prop won't be a column in database. its just for computaion
        public string PriceRange { get; set; } // if you remove this , then no add-migration is need as it not mapped to DB

        /*removed due to change requirement. to remove Catgory table and use BookDetail instead--------------------------

         //public Category Category { get; set; }//this creates Category_Id Column in book Table, index it and foreignKey it
         //but to define FK coulmn explicity do below

         [ForeignKey("Category")]
         public int Category_Id { get; set; }
         public Category Category { get; set; }//this creates Category_Id Column in book Table, index it and foreignKey it
        */


        [ForeignKey("BookDetail")]//1:1
        public int BookDetail_Id { get; set; } //BookDetail_Id column created and foreign keyed to BookDetail Table
        public BookDetail BookDetail { get; set; }

        [ForeignKey("Publisher")]//1:many
        public int Publisher_Id { get; set; }//each publish can publish many books
        public Publisher Publisher { get; set; }
    }
}
