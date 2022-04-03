using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    public class Author
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//default
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //explicity provide Id. DB won't generate
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Author_Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public ICollection<BookAuthor> BookAuthors { get; set; }// many:many relation between book and author
    }
}
