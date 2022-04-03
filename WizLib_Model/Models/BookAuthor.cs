using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    //many:many relationship > each book has many authors and each author has many books
    public class BookAuthor
    {
        //we must need primary key in EF model. here we use composite key instead because Book_Id,Author_Id combination is unique
        //[Key] on both Book_Id,Author_Id cant be used to define composite key
        //yoo can create composite key only using fluentAPI in DbContext

        [ForeignKey("Book")] //if this mistakenly not kept then you see Book_Id,Book_Id1 columns created
        public int Book_Id { get; set; }
        [ForeignKey("Author")]
        public int Author_Id { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
