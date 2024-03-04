using Microsoft.VisualBasic;
using System.Numerics;

namespace POE_PART1 
{
    class Program
    {
        private static void Main(string[] args)
        {
          
            Recipe recipe = new Recipe();
            recipe.Menu();
        }
    }

    class Recipe
    {
        private string recipeName;
        private string[] ingredients;
        private string[] steps;

        public Recipe()
        {
            ingredients = new string[0];
            steps = new string[0];
        }
        public void Menu()
        {
            int choice = 0;
            Console.WriteLine("Welcome to DoorNo's, the best recipe storage app ever made");
            Console.WriteLine("Our goal is to help you  stop spending your hard earned money on takeways, and teach you how to cook.");

            Console.WriteLine("Enter 1 to enter a recipe\n" +
                              "Enter 2 to Display a recipe\n" +
                              "Enter 3 to Adjust the scale of the recipe");
            choice = Convert.ToInt16(Console.ReadLine());
            while (choice != 9)
            {
                if (choice == 1)
                {
                    GetRecipe();
                }
                else if (choice == 2)
                {
                    DisplayRecipe();
                }
                else if (choice == 3)
                {
                    ScaleRecipe();
                }
                else if (choice == 9)
                {
                    break;
                }
            } 
        }
        
        public void GetRecipe()
        {
            Console.WriteLine("Welcome to the recipe app");
            Console.WriteLine("On the free plan, you have 1 recipe slot");

            Console.WriteLine("\nWhat is the name of your recipe?");
            recipeName = Console.ReadLine();
            int length = recipeName.Length;
            while ( length < 1)
            {
                Console.WriteLine("Enter a recipe name");
                recipeName = Console.ReadLine();
                length = recipeName.Length;
            }
            
            Console.WriteLine("List the ingredients in your recipe separated by a comma:");
            string allIngredients = Console.ReadLine();
            ingredients = allIngredients.Split(',');

            Console.WriteLine("List the quantities for all ingridients separated by a comma:");
            string quantities = Console.ReadLine();
            ingredients = quantities.Split(',');

            Console.WriteLine("How many steps does your recipe have?");
            int numSteps = 0;
            try
            {
                numSteps = int.Parse(Console.ReadLine());
            } catch
            {
                Console.WriteLine("Enter a number");
            }
            

            steps = new string[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                steps[i] = Console.ReadLine();
            }
        }
    
        public void DisplayRecipe()
        {
            Console.WriteLine($"Recipe Name: {recipeName}");
            Console.WriteLine("Ingredients:");
            foreach (string ingredient in ingredients)
            {
                Console.WriteLine(ingredient);
            }
            Console.WriteLine("Steps:");
            foreach (string step in steps)
            {
                Console.WriteLine(step);
            }
            Console.WriteLine();
        }
       
        public void ScaleRecipe()
        {
            Console.WriteLine("Enter the scale ");
            double factor = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Scaling recipe by a factor of {factor}...");
            
        }
       
        public void ResetQuantities()
        {
            Console.WriteLine("Resetting quantities to original...");
            
        }
  
        public void ClearRecipe()
        {
            Console.WriteLine("Clearing all data from the recipe...");
            
            recipeName = "";
            ingredients = new string[0];
            steps = new string[0];
        }
    }
}
//user enter details for 1 recipe in part 1
// needs to store number of ingrediants
// every ingreidant must have, name, quantity, and unit of measure
//number of steps
// every step - description of each step
// needs to display full recipe, with all details, and steps
// user can scale the recipe, by 0.5, 2, or 3. quanitities should change with the scale.
// user can reset quanities back to original
// user can clear all data from the recipe and enter a new recipe 
// only store in current memory, no txt document to store additonal info
// store ingrediants and steps in array