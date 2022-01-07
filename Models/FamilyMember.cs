using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FamilyApi.Models
{
    public class FamilyMember
    {
        public FamilyMember()
        {}

        public FamilyMember(Human human)
        {
            LastName = human.LastName;
            FirstName = human.FirstName;
            MidName = human.MidName;
            BirthDate = human.BirthDate;
            HumanID = human.ID;
            Gender = human.Gender;  
        }
        public int HumanID { get; set; }
        public Gender? Gender { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        public string FullName { get
            { return (LastName + " " + FirstName + " "+ MidName); }                
            }    
         

    }
}
