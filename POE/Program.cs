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
        double[] quantities;

        public Recipe()
        {
            ingredients = new string[0];
            steps = new string[0];
            quantities = new double[0];
        }
        public void Menu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("Welcome to DoorNo's, the best recipe storage app ever made");
                Console.WriteLine("Our goal is to help you stop spending your hard earned money on takeaways, and teach you how to cook.");

                Console.WriteLine("Enter 1 to Enter a recipe\n" +
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

            // Prompt for ingredients and quantities
            List<string> ingredientsList = new List<string>();
            List<double> quantitiesList = new List<double>();

            Console.WriteLine("Enter ingredients and quantities. Type 'done' when finished.");

            while (true)
            {
                Console.Write("Ingredient: ");
                string ingredient = Console.ReadLine();
                if (string.Equals(ingredient, "done", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                Console.Write("Quantity (grams/ml): ");
                if (!double.TryParse(Console.ReadLine(), out double quantity))
                {
                    Console.WriteLine("Invalid input for quantity. Please enter a valid number.");
                    continue;
                }

                // Add the ingredient and its quantity to the lists
                ingredientsList.Add(ingredient);
                quantitiesList.Add(quantity);
            }

            // Convert lists to arrays
            ingredients = ingredientsList.ToArray();
            quantities = quantitiesList.ToArray();

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
                Console.WriteLine(i + ") " + steps[i]);
            }
            Console.WriteLine();
        }

        public void ScaleRecipe()
        {
            Console.WriteLine("Enter the scale at which you would like to adjust the recipe:");
            Console.WriteLine("1 - Default scale (1)");
            Console.WriteLine("2 - Double scale (2)");
            Console.WriteLine("3 - Triple scale (3)");
            Console.WriteLine("4 - Half scale (0.5)");
            Console.WriteLine("5 - Reset scale to default (1)");

            try
            {
                double factor = Convert.ToDouble(Console.ReadLine());
                switch (factor)
                {
                    case 1:
                        // No scaling needed, already at default scale
                        break;
                    case 2:
                        ScaleIngredients(2); // Double scale
                        break;
                    case 3:
                        ScaleIngredients(3); // Triple scale
                        break;
                    case 4:
                        ScaleIngredients(0.5); // Half scale
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

        private void ScaleIngredients(double factor)
        {
            for (int i = 0; i < quantities.Length; i++)
            {
                quantities[i] *= factor; // Scale each quantity
            }
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