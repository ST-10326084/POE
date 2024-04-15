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
            Recipe recipe = new Recipe();
            recipe.Menu();
            
        }
    }

    class Recipe
    {
        // Arrays for all values in the recipe
        public string RecipeName { get; set; } // stores the recipe name
        public string[] Ingredients { get; set; } // stores the recipe ingredients
        public string[] Steps { get; set; } // stores the recipe steps
        private double[] originalQuantities; // Keep original quantities for scaling
        public double[] Quantities { get; set; } // stores the recipe quantities
        public string[] unitOfMeasure { get; set; } // stores the recipe unit of measurement
        private bool recipeEntered = false; // ensure user doesnt overwrite their recipe.
        private int ingredientCount; // capture amount of ingrediants stored, used for dynamic array size, will be removed once arraylists are added

        public Recipe()
        {
            // initialize arrays
            Ingredients = new string[0];
            Steps = new string[0];
            Quantities = new double[0];
            unitOfMeasure = new String[0];
        }

        public void Menu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\x1b[34mWelcome to DoorNo's, the best recipe storage app ever made\x1b[0m");
               
                Console.WriteLine("Our goal is to help you stop spending your hard earned money on takeaways, and teach you how to cook.\n");

                Console.WriteLine("Enter 1 to Enter a recipe\n" +
                                  "Enter 2 to Display a recipe\n" +
                                  "Enter 3 to Adjust the scale of the recipe\n" +
                                  "Enter 4 to Clear all data and enter a new recipe\n" +
                                  "Enter 9 to Exit");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice) // swicth is a decent way to have a user choose the path of entry into the program, once guis are introduced, this can be cleaned up
                    {
                        case 1:
                            if (!recipeEntered)
                            {
                                getRecipe();
                                recipeEntered = true;
                            }
                            else
                            {
                                Console.WriteLine("Please clear the existing recipe first.");
                            }
                            break;
                        case 2:
                            displayRecipe();
                            break;
                        case 3:
                            scaleRecipe();
                            break;
                        case 4:
                            clearRecipe();
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

        public void getRecipe()
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

            // Get ingredients
            getIngrediants();
            Console.WriteLine("Ingredients captured. Press any key to continue..."); // make users acknowlege that they have entered all recipe ingrediants
            Console.ReadKey(true);

            // Get steps
            getSteps();
            Console.WriteLine("Steps Captured.");
        }      

        public void getIngrediants()
        {
            // this method becomes significantly better when arraylists and lists are introduced, was not allowed to be used here according to lecturer

            const int initialCapacity = 5; // Initial capacity for arrays to make them dynamic 
            int ingredientCount = 0; // Tracks the number of ingredients entered

            // temporary arrays to store user input
            string[] tempIngredients = new string[initialCapacity];
            double[] tempQuantities = new double[initialCapacity];
            string[] tempUnitOfMeasure = new string[initialCapacity];

            Console.WriteLine("Enter ingredients and quantities and unit of measurement. Type 'done' in the ingredient section when finished.");

            while (true)
            {
                // capture ingrediants
                Console.Write("Ingredient: ");
                string ingredient = Console.ReadLine();
                if (string.Equals(ingredient, "done", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (ingredientCount >= tempIngredients.Length)
                {
                    // Resize arrays (by double to ensure there is enough space for more ingrediants should they exceede the current max of 5)
                    Array.Resize(ref tempIngredients, tempIngredients.Length * 2);
                    Array.Resize(ref tempQuantities, tempQuantities.Length * 2);
                    Array.Resize(ref tempUnitOfMeasure, tempUnitOfMeasure.Length * 2);
                }
                // capture quantity
                Console.Write("Quantity: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity))
                {
                    Console.WriteLine("Invalid input for quantity. Please enter a valid number.");
                    continue;
                }

                // capture UoM
                Console.Write("Unit of Measure: ");
                string unitOfMeasure = Console.ReadLine();

                // Capitalize the first letter of the ingredient for a neater end product
                ingredient = char.ToUpper(ingredient[0]) + ingredient.Substring(1).ToLower();

                // Add the ingredient and its quantity to the arrays
                tempIngredients[ingredientCount] = ingredient;
                tempQuantities[ingredientCount] = quantity;
                tempUnitOfMeasure[ingredientCount] = unitOfMeasure;

                ingredientCount++;
            }

            // Resize arrays to the actual count
            Ingredients = new string[ingredientCount];
            Quantities = new double[ingredientCount];
            unitOfMeasure = new string[ingredientCount];
            originalQuantities = new double[ingredientCount]; // Array to store original quantities

            // copies data into the resized arrays
            Array.Copy(tempIngredients, Ingredients, ingredientCount);
            Array.Copy(tempQuantities, Quantities, ingredientCount);
            Array.Copy(tempUnitOfMeasure, unitOfMeasure, ingredientCount);
            Array.Copy(tempQuantities, originalQuantities, ingredientCount); // Copy quantities to originalQuantities
        }

        public void getSteps()
        {
     
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

        public void displayRecipe()
        {
            // display method to show the user what they have typed
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m"); 
            Console.WriteLine($"Recipe Name: {RecipeName}");
            Console.WriteLine("Ingredients:");

            for (int i = 0; i < Ingredients.Length; i++)
            {
                Console.WriteLine($"{Quantities[i]} {unitOfMeasure[i]} of {Ingredients[i]}");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Length; i++)
            {
                Console.WriteLine(i + 1 + ") " + Steps[i]);
            }
            Console.WriteLine();
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m"); // colour to ensure display is readable and neat. stands out from other text in console
        }

        public void scaleRecipe()
        {
            Console.WriteLine("\nEnter the scale at which you would like to adjust the recipe:");
            Console.WriteLine("1 - Default scale (1)");
            Console.WriteLine("2 - Double scale (2)");
            Console.WriteLine("3 - Triple scale (3)");
            Console.WriteLine("4 - Half scale (0.5)");
            Console.WriteLine("5 - Reset scale to default (1)");

            // ensure a recipe has been entered
            if (recipeEntered == true)
            {
                try
                {
                    double factor = Convert.ToDouble(Console.ReadLine());
                    switch (factor)
                    {
                        case 1:
                            // Reset quantities to original values
                            Quantities = originalQuantities.Clone() as double[]; // could remove 1 or 5, as scaling by 1 or resetting is the same thing 
                            break;
                        case 2:
                            scaleIngredients(2); // Double scale
                            break;
                        case 3:
                            scaleIngredients(3); // Triple scale
                            break;
                        case 4:
                            scaleIngredients(0.5); // Half scale
                            break;
                        case 5:
                            // Reset quantities to original values
                            Quantities = originalQuantities.Clone() as double[]; 
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

        private void scaleIngredients(double factor)
        {
            for (int i = 0; i < Quantities.Length; i++)
            {
                Quantities[i] *= factor; // Scale each quantity
            }
        }

        public void clearRecipe()
        {
            String choice;
            Console.WriteLine("Are you sure you want to clear a recipe, type 'yes' to clear");
            choice = Console.ReadLine();
            choice = choice.ToLower(); // ensures its not case sensitive

            if (choice == "yes")
            {
                Console.WriteLine("\u001b[32mAll data cleared from recipe.\n\u001b[0m");
                
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