using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    public class Category
    {
        //public int Id { get; set; }  //if the name ends with Id, then its by default primary key with identity. else mention [Key]


        public int Id { get; set; }
        public string Name { get; set; }
    }
}
