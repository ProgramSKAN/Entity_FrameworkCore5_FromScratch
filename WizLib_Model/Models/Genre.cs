﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Model.Models
{
    public class Genre
    {
        public int GenreId { get; set; }// GenreId> primary key automatically since name ends with Id.Genre_Id won't detect as primary key
        public string GenreName { get; set; }
        //public int DisplayOrder { get; set; }
    }
}