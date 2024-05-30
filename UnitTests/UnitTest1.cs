using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace POE_PART1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CalculateTotalCalories_ShouldReturnCorrectCalorieCount()
        {         

            // First Recipe: Spaghetti Bolognese
            var ChickenBrocoli = new Recipe("Chicken Brocoli");

            ChickenBrocoli.AddIngredient(new Ingredient("Spaghetti", 200, "grams", 350, "Carbohydrates"));
            ChickenBrocoli.AddIngredient(new Ingredient("Ground Beef", 250, "grams", 250, "Proteins"));

            ChickenBrocoli.Steps.Add("Boil spaghetti until al dente.");
            ChickenBrocoli.Steps.Add("Cook ground beef with seasoning.");
            ChickenBrocoli.Steps.Add("Mix spaghetti with beef and tomato sauce.");
            ChickenBrocoli.Steps.Add("Serve with grated cheese.");

            recipes.Add(ChickenBrocoli);
            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(350, totalCalories);
        }
    }
}
