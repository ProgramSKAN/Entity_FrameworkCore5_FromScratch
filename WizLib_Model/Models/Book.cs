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
    }
}
