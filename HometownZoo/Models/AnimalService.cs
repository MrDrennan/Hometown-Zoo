using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HometownZoo.Models
{
    public static class AnimalService
    {
        /// <summary>
        /// Returns All animals from the database sorted by name in ascending order
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<Animal> GetAnimals(ApplicationDbContext db)
        {
            //method syntax
            return db.Animals
                .OrderBy(a => a.Name)
                .ToList();

            // query syntax
            //    return (from a in db.Animals
            //            orderby a.Name
            //            select a).ToList();
            //}
        }
    }
}