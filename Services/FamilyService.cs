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
                relations = await _context.Relations
                    .Where(r => r.HumanID.Equals(humanID))
                    .Include(r => r.Kin)
                    .AsNoTracking().ToListAsync();
            
            } else
            {

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

        }



        public async Task<ActionResult<Human>> Get(int id)

        {
            var human = await _context.Humans.FindAsync(id);

 

            return human;
        }
    }
}
