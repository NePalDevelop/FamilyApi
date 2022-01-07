namespace FamilyApi.Models
{
    //public enum Kindred
    //{
    //    Father, Mother, Son, Daughter, Grandpa, Grandma, Sister, Brother
    //}
    public class FamilyRelation
    {
        public Kindred? Kindred { get; set; }

 //       public FamilyMember Human { get; set; }

        public FamilyMember Kin { get; set; }
    }
}