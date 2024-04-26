using System;
using System.Collections.Generic;

namespace POE_PART1
{
    class Program
    {
        private static void Main(string[] args)
        {
            Recipe recipe = new Recipe();

            // Subscribe to the OnHighCalories event
            recipe.OnHighCalories += (message) => Console.WriteLine(message);

            recipe.Menu();
        }
    }

    class Recipe
    {
        public List<string> RecipeName { get; set; } // stores the recipe name
        public List<string> Ingredients { get; set; } // stores the recipe ingredients
        public List<string> Steps { get; set; } // stores the recipe steps
        private List<double> OriginalQuantities { get; set; } // Keep original quantities for scaling
        public List<double> Quantities { get; set; } // stores the recipe quantities
        public List<string> UnitOfMeasure { get; set; } // stores the recipe unit of measurement
        public List<double> Calories { get; set; } // New list for storing calories per ingredient
        public List<string> FoodGroups { get; set; } // New list for storing food groups per ingredient
        private bool recipeEntered = false; // ensure user doesn't overwrite their recipe


        // Define a delegate for calorie notifications
        public delegate void CalorieNotificationHandler(string message);
        public event CalorieNotificationHandler OnHighCalories;

        public Recipe()
        {
            // initialize lists
            RecipeName = new List<string>();
            Ingredients = new List<string>();
            Steps = new List<string>();
            Quantities = new List<double>();
            UnitOfMeasure = new List<string>();
            OriginalQuantities = new List<double>();
            Calories = new List<double>(); 
            FoodGroups = new List<string>();
        }

        public void Menu()
        {
            int choice = 0;

            do
            {
                Console.WriteLine("\x1b[34m\nWelcome to DoorNo's, the best recipe storage app ever made\x1b[0m");

                Console.WriteLine("Our goal is to help you stop spending your hard earned money on takeaways, and teach you how to cook.\n");

                Console.WriteLine("Enter 1 to Enter a recipe\n" +
                                  "Enter 2 to Display a recipe\n" +
                                  "Enter 3 to Adjust the scale of the recipe\n" +
                                  "Enter 4 to Clear all data and enter a new recipe\n" +
                                  "Enter 9 to Exit");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice) // switch is a decent way to have a user choose the path of entry into the program, once guis are introduced, this can be cleaned up
                    {
                        case 1:
                            GetRecipe();
                            break;
                        case 2:
                            displayRecipe();
                            break;
                        case 3:
                            scaleRecipe();
                            break;
                        case 4:
                            clearRecipe();
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
            Console.WriteLine("What is the name of your recipe?");

            string newRecipeName = Console.ReadLine(); // to ensure that new recipes are not duplicates, and to ensure they are not empty

            while (string.IsNullOrEmpty(newRecipeName))
            {
                Console.WriteLine("Please enter a recipe name:");
                newRecipeName = Console.ReadLine();
            }

            if (RecipeName.Contains(newRecipeName))
            {
                Console.WriteLine($"The recipe '{newRecipeName}' already exists. Please use a different name.");
                return;
            }

            RecipeName.Add(newRecipeName);

            // Get ingredients
            GetIngredients();
            Console.WriteLine("Ingredients captured. Press any key to continue...");
            Console.ReadKey();

            // Get steps
            GetSteps();
            Console.WriteLine("Steps captured.");
        }


        public void GetIngredients()
        {
            Console.WriteLine("Enter ingredients, quantities, and units of measurement. Type 'done' to finish.");

            while (true)
            {
                Console.Write("Ingredient: ");
                string ingredient = Console.ReadLine();

                if (string.Equals(ingredient, "done", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                Console.Write("Quantity: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity))
                {
                    Console.WriteLine("Invalid input for quantity. Please enter a valid number.");
                    continue;
                }

                Console.Write("Unit of Measure: ");
                string units = Console.ReadLine();

                Console.Write("Calories: "); // New prompt for calories
                if (!double.TryParse(Console.ReadLine(), out double calories))
                {
                    Console.WriteLine("Invalid input for calories. Please enter a valid number.");
                    continue;
                }

                Console.Write("Food Group: "); // New prompt for food group
                string foodGroup = Console.ReadLine();

                Ingredients.Add(ingredient);
                Quantities.Add(quantity);
                OriginalQuantities.Add(quantity);
                UnitOfMeasure.Add(units);
                Calories.Add(calories);
                FoodGroups.Add(foodGroup); 

                // add calories here 
                // prompt users for calories, use a delegate to alert if over 300 cal.
                // need to display in the display method the total calories per recipe.

                // add a food group here
                // prompt user for food group

                
            }
        }

        public void GetSteps()
        {
            Console.WriteLine("How many steps does your recipe have?");
            if (!int.TryParse(Console.ReadLine(), out int numSteps))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            Steps = new List<string>(numSteps);
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                Steps.Add(Console.ReadLine());
            }
        }
        /*
        public void displayRecipe()
        {

            // allow unlimited recipes now, user must select what recipe they want to see. organise recipes by alphabetical order
            // use a delegate to notify user if above 300 calories
            // create unit test to test the calorie calculation works. best done in a seperate method

            // display method to show the user what they have typed
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
            Console.WriteLine($"Recipe Name: {RecipeName}");
            Console.WriteLine("Ingredients:");

            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{Quantities[i]} {UnitOfMeasure[i]} of {Ingredients[i]}");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + Steps[i]);
            }
            Console.WriteLine();
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m"); // colour to ensure display is readable and neat. stands out from other text in console
        }
        */
        public void displayRecipe()
        {
            if (RecipeName.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            // Sort recipes alphabetically
            List<string> sortedRecipeNames = RecipeName.OrderBy(name => name).ToList();

            // Display all available recipe names
            Console.WriteLine("Available Recipes:");
            for (int i = 0; i < sortedRecipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {sortedRecipeNames[i]}");
            }

            Console.Write("Select a recipe to display (enter the corresponding number): ");
            if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex < 1 || recipeIndex > sortedRecipeNames.Count)
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.");
                return;
            }

            // Find the recipe to display
            string selectedRecipeName = sortedRecipeNames[recipeIndex - 1];
            int recipeIdx = RecipeName.IndexOf(selectedRecipeName);

            // Calculate total calories
            double totalCalories = Calories.Sum();

            if (totalCalories > 300) // If total calories exceed 300, trigger the event
            {
                OnHighCalories?.Invoke($"Warning: Recipe '{selectedRecipeName}' exceeds 300 calories with a total of {totalCalories} calories.");
            }

            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
            Console.WriteLine($"Recipe Name: {selectedRecipeName}");
            Console.WriteLine("Ingredients:");

            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{Quantities[i]} {UnitOfMeasure[i]} of {Ingredients[i]} ({Calories[i]} calories, {FoodGroups[i]} food group)");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {Steps[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
        }

        public void scaleRecipe()
        {
            Console.WriteLine("\nEnter the scale at which you would like to adjust the recipe:");
            Console.WriteLine("1 - Default scale (1)");
            Console.WriteLine("2 - Double scale (2)");
            Console.WriteLine("3 - Triple scale (3)");
            Console.WriteLine("4 - Half scale (0.5)");
            Console.WriteLine("5 - Reset scale to default (1)");

            if (recipeEntered)
            {
                try
                {
                    double factor = Convert.ToDouble(Console.ReadLine());

                    switch (factor)
                    {
                        case 1:
                        case 5:
                            // Reset quantities to original values
                            Quantities = new List<double>(OriginalQuantities); // Use constructor to copy list
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
            else
            {
                Console.WriteLine("Enter a recipe before trying to scale it.");
            }
        } 

        private void scaleIngredients(double factor)
        {
            for (int i = 0; i < Quantities.Count; i++)
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
                // need to ask what recipe they want to clear in the array.
                // and only delete that one, not the whole array
                /*RecipeName = "";
                Ingredients = new string[0];
                Steps = new string[0];
                Quantities = new double[0]; // Reset quantities\
                // NEED TO CLEAR A SPECIFIC INDEX, cant just reset the whole list
                Quantities.Clear();
                */
            }
            else
            {
                Console.WriteLine("\u001b[31mYour recipe was NOT cleared, enter 'yes' to clear\n\u001b[0m");
            }
        }
    }
}
