using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyApi.Models
{       
    public enum Gender
        {
            Man, 
            Woman
        }

    public class Human
    {
        public int ID { get; set; }
        public Gender? Gender { get; set; }  
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        //public Relation RelationKin { get; set; }   
        public ICollection<Relation> Relations { get; set; }
    }
}
