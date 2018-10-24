using Microsoft.VisualStudio.TestTools.UnitTesting;
using HometownZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace HometownZoo.Models.Tests
{
    [TestClass()]
    public class AnimalServiceTests
    {
        private IQueryable<Animal> animals;

        [TestInitialize()] // runs before each test
        public void BeforeTest()
        {
            // AsQueryable converst List to IQueryable
            animals = new List<Animal>()
            {
                new Animal(){ AnimalId = 1, Name = "Zebra"},
                new Animal(){AnimalId = 2, Name = "Bat"}
            }.AsQueryable();
        }

        [TestMethod]
        public void GetAnimals_ShouldReturnAllAnimalsSortedByName()
        {
            // Set up Mock database and mock animal table

            // create a Mock of Animals
            Mock<DbSet<Animal>> mockAnimals = GetAnimalMockDbSet();

            // create mock database
            // .Object returns the Object within the generic type
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals)
                    .Returns(mockAnimals.Object);

            // Act
            IEnumerable<Animal> allAnimals = AnimalService.GetAnimals(mockDb.Object);

            // Assert all animals are returned
            Assert.AreEqual(2, allAnimals.Count());

            // Assert animals are sorted by name (ascending)
            Assert.AreEqual("Bat", allAnimals.ElementAt(0).Name);
            Assert.AreEqual("Zebra", allAnimals.ElementAt(1).Name);
        }

        private Mock<DbSet<Animal>> GetAnimalMockDbSet()
        {
            Mock<DbSet<Animal>> mockAnimals = new Mock<DbSet<Animal>>();
            mockAnimals.As<IQueryable<Animal>>()
                    .Setup(m => m.Provider)
                    .Returns(animals.Provider);

            mockAnimals.As<IQueryable<Animal>>()
                    .Setup(m => m.Expression)
                    .Returns(animals.Expression);

            mockAnimals.As<IQueryable<Animal>>()
                    .Setup(m => m.GetEnumerator())
                    .Returns(animals.GetEnumerator());

            return mockAnimals;
        }
    }
}