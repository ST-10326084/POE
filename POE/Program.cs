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
        string[] quantities;

        public Recipe()
        {
            ingredients = new string[0];
            steps = new string[0];
            quantities = new string[0];
        }
        public void Menu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("Welcome to DoorNo's, the best recipe storage app ever made");
                Console.WriteLine("Our goal is to help you stop spending your hard earned money on takeaways, and teach you how to cook.");

                Console.WriteLine("Enter 1 to enter a recipe\n" +
                                  "Enter 2 to Display a recipe\n" +
                                  "Enter 3 to Adjust the scale of the recipe\n" +
                                  "Enter 4 to Clear all data and enter a new recipe\n" +
                                  "Enter 9 to Exit");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            GetRecipe();
                            break;
                        case 2:
                            DisplayRecipe();
                            break;
                        case 3:
                            ScaleRecipe();
                            break;
                        case 4:
                            ClearRecipe();
                            break;
                        case 9:
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            } while (choice != 9);
        }

        public void GetRecipe()
        {
            Console.WriteLine("Welcome to the recipe app");
            Console.WriteLine("On the free plan, you have 1 recipe slot");

            Console.WriteLine("\nWhat is the name of your recipe?");
            recipeName = Console.ReadLine();

            while (string.IsNullOrEmpty(recipeName))
            {
                Console.WriteLine("Enter a recipe name");
                recipeName = Console.ReadLine();
            }

            Console.WriteLine("List the ingredients in your recipe separated by a comma:");
            string allIngredients = Console.ReadLine();
            ingredients = allIngredients.Split(',');

            // Get quantities for each ingredient
            Console.WriteLine("List the quantities for all ingredients separated by a comma(grams/ml:");
            string allQuantities = Console.ReadLine();
            quantities = allQuantities.Split(','); // needs to be an int, to scale it !!!

            if (quantities.Length != ingredients.Length)
            {
                Console.WriteLine("Invalid input. Number of quantities must match number of ingredients.");
                return;
            }

            Console.WriteLine("How many steps does your recipe have?");
            int numSteps = 0;
            if (!int.TryParse(Console.ReadLine(), out numSteps))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
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

            for (int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine($"{ingredients[i]} - {quantities[i]}g");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine(i+") "+steps[i]);
            }
            Console.WriteLine();
        }

        public void ScaleRecipe()
        {
            // add menu here, to reset scale, and adjust it (0.5, 2 and 3) if statements or switch
            Console.WriteLine("Enter the scale at which you would like to adjust the recipe too. ");
            Console.WriteLine("Enter 1 for default scale (1)");
            Console.WriteLine("Enter 2 for double scale (2)");
            Console.WriteLine("Enter 3 for triple scale (3)");
            Console.WriteLine("Enter 4 for half scale (0.5)");
            //double factor = Convert.ToDouble(Console.ReadLine());


            //Console.WriteLine($"Scaling recipe by a factor of {factor}...");
            try
            {
                double factor = Convert.ToInt32(Console.ReadLine());
                switch (factor)
                {
                    case 1:
                        
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
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