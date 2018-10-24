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

        public static void AddAnimal(Animal a, ApplicationDbContext db)
        {
            // is checks a type (a little faster than ==)
            if (a is null)
            {
                throw new ArgumentNullException($"Parameter {nameof(a)} cannot be null");
            }

            //TODO: Ensure duplicate names are disallowed

            db.Animals.Add(a);
            db.SaveChanges();
        }
    }
}