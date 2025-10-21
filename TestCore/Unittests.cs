using Grocery.Core.Data.Repositories;
using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Services;

namespace TestCore
{
    public class Unittests
    {
        //ProductService _productService;

        [SetUp]
        public void Setup()
        {
            //_productService = new(new ProductRepository());
        }


        //Happy flow
        [TestCase("Kaas", 40, 2026, 12, 30, 3.50)]
        [TestCase("Hagelslag", 1289, 2596, 1, 15, 0.00)]
        [TestCase("G", 0, 2025, 12, 30, 55.35)]
        public void TestInfoValidationReturnsTrue(string name, int stock, int year, int month, int day, decimal price)
        {
            DateOnly shelfLife = new(year, month, day);
            bool result = ProductHelper.CheckProductInfo(name, stock, shelfLife, price);
            Assert.IsTrue(result);
        }


        // Unhappy flow
        [TestCase("", 40, 2026, 12, 30, 3.50)] // To test an empty name
        [TestCase("This name has to be eighty characters long, this way the method won’t accept it.", 40, 2026, 12, 30, 3.50)] // To test a name of 80 characters
        [TestCase("This name needs to be longer than eighty characters, that way the method won’t accept it.", 40, 2026, 12, 30, 3.50)] // To test a name longer than 80 characters
        [TestCase("Kaas", -40, 2026, 12, 30, 3.50)] // To test the stock
        [TestCase("Kaas", 40, 2025, 05, 13, 3.50)] // To test the date
        [TestCase("Kaas", 40, 2026, 12, 30, -3.50)] // To test the price
        public void TestInfoValidationReturnsFalse(string name, int stock, int year, int month, int day, decimal price)
        {
            DateOnly shelfLife = new(year, month, day);
            bool result = ProductHelper.CheckProductInfo(name, stock, shelfLife, price);
            Assert.IsFalse(result);
        }

    }
}