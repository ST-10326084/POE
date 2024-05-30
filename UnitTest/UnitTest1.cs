using NUnit.Framework;
using POE_PART1;

namespace UnitTest
{
    public class Tests
    {
        private RecipeBook recipeBook;

        [SetUp]
        public void Setup()
        {
            recipeBook = new RecipeBook();
        }

        [Test]
        public void SpaghettiBolognese_Calories_ShouldBeCorrect()
        {
            // Arrange
            var recipe = recipeBook.GetRecipes().Find(r => r.Name == "Spaghetti Bolognese");

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(600, totalCalories); // 350 (Spaghetti) + 250 (Ground Beef)
        }

        [Test]
        public void CaesarSalad_Calories_ShouldBeCorrect()
        {
            // Arrange
            var recipe = recipeBook.GetRecipes().Find(r => r.Name == "Caesar Salad");

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(200, totalCalories); // 20 (Romaine Lettuce) + 180 (Caesar Dressing)
        }
    }
}
