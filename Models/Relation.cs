
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyApi.Models
{
    public enum Kindred
    {
        Father, Mother, Son, Daughter, Grandpa, Grandma, Sister, Brother
    }
    public class Relation
    {
        public int RelationID { get; set; }
        public int HumanID { get; set; }
        public int KinID { get; set; }
        public Kindred? Kindred { get; set; }

        public Human Human { get; set; }
      
//        [NotMapped]
        public Human Kin { get; set; }
    }

 }
