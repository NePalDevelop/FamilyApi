using System;
using FamilyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FamilyApi.Services
{
    public class FamilyService
    {
        private readonly FamilyApiContext _context;

        public FamilyService(FamilyApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Human>>> Get(string searchstring)
        {
            
            IQueryable<Human> humanIQ = from h in _context.Humans
                                        select h;
            if (!String.IsNullOrEmpty(searchstring))
            {
                humanIQ = humanIQ.Where(s => s.LastName.Contains(searchstring)
                                       || s.FirstName.Contains(searchstring)
                                       || s.MidName.Contains(searchstring));
            }
           
            return await humanIQ.AsNoTracking().ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<Relation>>> GetFamily(int? humanID, int? parentsOrKind)
        {
//            IQueryable<Relation> query;

            List<Relation> relations;

            if (!humanID.HasValue) return new List<Relation>();
;

            if (parentsOrKind == 0)
            {
                //query = from r in _context.Relations
                //        join h in _context.Humans on r.HumanID equals h.ID
                //        join k in _context.Humans on r.KinID equals k.ID
                //        where r.HumanID == humanID
                //        select 
                //        new Relation
                //          {
                //            RelationID = r.RelationID,
                //            Kindred = r.Kindred,
                //            HumanID = h.ID,
                //            KinID = k.ID,
                //            Human = h,
                //            Kin = k
                //             };

                relations = await _context.Relations
                    .Where(r => r.HumanID.Equals(humanID))
                    .Include(r => r.Kin)
                    .AsNoTracking().ToListAsync();
            
            } else
            {

                 //query = from r in _context.Relations
                 //                join h in _context.Humans on r.KinID equals h.ID
                 //                join k in _context.Humans on r.HumanID equals k.ID
                 //                where r.KinID == humanID
                 //                select new Relation
                 //                {
                 //                    Kindred = k.Gender == Gender.Man ? Kindred.Son : Kindred.Daughter,
                 //                    RelationID = r.RelationID,
                 //                    HumanID = h.ID,
                 //                    KinID = k.ID,
                 //                    Human = h,
                 //                    Kin = k
                 //                };

                relations = await _context.Relations
                    .Where(r => r.KinID.Equals(humanID))
                    .Include(r => r.Human)
                    .AsNoTracking().ToListAsync();
                foreach (var rel in relations)
                {
                    rel.Kin = rel.Human;
                    rel.Human = null;
                    rel.KinID = rel.HumanID;
                    rel.HumanID = (int)humanID;
                    rel.Kindred = rel.Kin.Gender == Gender.Man ? Kindred.Son : Kindred.Daughter;
                    rel.Kin.Relations = null;
                }
                var hh = humanID;
            }


            return relations;
//                await query.AsNoTracking().ToListAsync();
        }



        public async Task<ActionResult<Human>> Get(int id)

        {
            var human = await _context.Humans.FindAsync(id);

 

            return human;
        }
    }
}
