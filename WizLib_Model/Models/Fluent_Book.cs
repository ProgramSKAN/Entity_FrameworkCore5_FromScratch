using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    public class Fluent_Book
    {
        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }

        //Book -Book-detail 1:1 relation made in fluent api 
        public int BookDetail_Id { get; set; }
        public Fluent_BookDetail Fluent_BookDetail { get; set; }

        //book - publisher  1:many relation made in fluent api
        public int Publisher_Id { get; set; }
        public Fluent_Publisher Fluent_Publisher { get; set; }

        //book - author  many : many relation. BookAuthor intermediate table
        public ICollection<Fluent_BookAuthor> Fluent_BookAuthors { get; set; }

    }
}
