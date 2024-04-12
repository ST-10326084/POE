using Microsoft.VisualBasic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace POE_PART1
{
    class Program
    {
        private static void Main(string[] args)
        {
            name name = new name();
            name.Age();

            Recipe recipe = new Recipe();
            recipe.Menu();
            
        }
    }

    class Recipe
    {
        public string RecipeName { get; set; }
        public string[] Ingredients { get; set; }
        public string[] Steps { get; set; }
        private double[] originalQuantities; // Keep original quantities for scaling
        public double[] Quantities { get; set; }
        private bool recipeEntered = false; // ensure user doesnt overwrite their recipe.

        public Recipe()
        {
            Ingredients = new string[0];
            Steps = new string[0];
            Quantities = new double[0];
        }

        public void Menu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\x1b[34mWelcome to DoorNo's, the best recipe storage app ever made\x1b[0m");
                //Console.WriteLine("Welcome to DoorNo's, the best recipe storage app ever made");
                Console.WriteLine("Our goal is to help you stop spending your hard earned money on takeaways, and teach you how to cook.\n");

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
                            if (!recipeEntered)
                            {
                                GetRecipe();
                                recipeEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Please clear the existing recipe first.");
                            }
                            break;
                        case 2:
                            DisplayRecipe();
                            break;
                        case 3:
                            ScaleRecipe();
                            break;
                        case 4:
                            ClearRecipe();
                            recipeEntered = false;
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
            
            Console.WriteLine("\nWelcome to the recipe app");
            Console.WriteLine("On the free plan, you have 1 recipe slot");

            Console.WriteLine("\nWhat is the name of your recipe?");
            RecipeName = Console.ReadLine();

            while (string.IsNullOrEmpty(RecipeName))
            {
                Console.WriteLine("Enter a recipe name");
                RecipeName = Console.ReadLine();
            }

            // Prompt for ingredients and quantities
            List<string> ingredientsList = new List<string>(); // using lists to simplify (could also have used ArrayLists
            List<double> quantitiesList = new List<double>();

            Console.WriteLine("Enter ingredients and quantities. Type 'done' when finished.");

            while (true)
            {
                Console.Write("Ingredient: ");
                string ingredient = Console.ReadLine();
                if (string.Equals(ingredient, "done", StringComparison.OrdinalIgnoreCase)) // ignore case to ensure any variation of done works 
                {
                    break;
                }

                Console.Write("Quantity (grams/ml): "); // here we need to make it so that a user can enter a number and specify the scale afterward. such as 1 cup, 3 teaspoons. etc
                if (!double.TryParse(Console.ReadLine(), out double quantity))
                {
                    Console.WriteLine("Invalid input for quantity. Please enter a valid number.");
                    continue;
                }

                // Capitalize the first letter of the ingredient // String manipulation
                ingredient = char.ToUpper(ingredient[0]) + ingredient.Substring(1).ToLower();

                // Add the ingredient and its quantity to the lists
                ingredientsList.Add(ingredient);
                quantitiesList.Add(quantity);
            }

            // Convert lists to arrays
            Ingredients = ingredientsList.ToArray();
            Quantities = quantitiesList.ToArray();
            originalQuantities = Quantities.Clone() as double[]; // store oringal values to be used for reset (scaling)

            if (Quantities.Length != Ingredients.Length)
            {
                Console.WriteLine("Invalid input. Number of quantities must match number of ingredients.");
                return;
            }

            Console.WriteLine("\nHow many steps does your recipe have?");
            int numSteps = 0;
            if (!int.TryParse(Console.ReadLine(), out numSteps))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return;
            }

            Steps = new string[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                Steps[i] = Console.ReadLine();
            }
        }

        public void DisplayRecipe()
        {
            
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m"); 
            Console.WriteLine($"Recipe Name: {RecipeName}");
            Console.WriteLine("Ingredients:");

            for (int i = 0; i < Ingredients.Length; i++)
            {
                Console.WriteLine($"{Ingredients[i]} - {Quantities[i]}g");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Length; i++)
            {
                Console.WriteLine(i + 1 + ") " + Steps[i]);
            }
            Console.WriteLine();
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
        }

        public void ScaleRecipe()
        {
            Console.WriteLine("\nEnter the scale at which you would like to adjust the recipe:");
            Console.WriteLine("1 - Default scale (1)");
            Console.WriteLine("2 - Double scale (2)");
            Console.WriteLine("3 - Triple scale (3)");
            Console.WriteLine("4 - Half scale (0.5)");
            Console.WriteLine("5 - Reset scale to default (1)");
            //if  need to add a system where if no recipe is entered, then no scaling options can be selected
            if (recipeEntered == true)
            {
                try
                {
                    double factor = Convert.ToDouble(Console.ReadLine());
                    switch (factor)
                    {
                        case 1:
                            // Reset quantities to original values
                            Quantities = originalQuantities.Clone() as double[];
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
                        case 5:
                            // Reset quantities to original values
                            Quantities = originalQuantities.Clone() as double[]; // dont need duplicated reset
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
            } else {
                   Console.WriteLine("Enter a recipe before trying to scale it");
                   }
        }

        private void ScaleIngredients(double factor)
        {
            for (int i = 0; i < Quantities.Length; i++)
            {
                Quantities[i] *= factor; // Scale each quantity
            }
        }

        public void ClearRecipe()
        {
            String choice;
            Console.WriteLine("Are you sure you want to clear a recipe, type 'yes' to clear");
            choice = Console.ReadLine();
            choice = choice.ToLower();
            if (choice == "yes")
            {
                Console.WriteLine("\u001b[32mAll data cleared from recipe.\n\u001b[0m");
                // add a confirmation here to ensure they want to clear the text ( simple if 

                RecipeName = "";
                Ingredients = new string[0];
                Steps = new string[0];
                Quantities = new double[0]; // Reset quantities
            } else
            {
                Console.WriteLine("\u001b[31mYour recipe was NOT cleared, enter 'yes' to clear\n\u001b[0m");
            }
        }
    }
}


//read list
// seperate into different classes ( different pages)
// to add 
// priority developement: (top down)
// a system where it takes the grams/ ml and gives a answer such as 250ml/g = a cup, or where 25ml a table spoon, etc 
// automatic properties to solve a problem (scale? ) 
// separate classes onto diffrent 'pages'
// reference

