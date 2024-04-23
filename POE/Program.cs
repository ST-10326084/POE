using System;
using System.Collections.Generic;

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
        public string RecipeName { get; set; } // stores the recipe name
        public List<string> Ingredients { get; set; } // stores the recipe ingredients
        public List<string> Steps { get; set; } // stores the recipe steps
        private List<double> OriginalQuantities { get; set; } // Keep original quantities for scaling
        public List<double> Quantities { get; set; } // stores the recipe quantities
        public List<string> UnitOfMeasure { get; set; } // stores the recipe unit of measurement
        private bool recipeEntered = false; // ensure user doesn't overwrite their recipe

        public Recipe()
        {
            // initialize lists
            Ingredients = new List<string>();
            Steps = new List<string>();
            Quantities = new List<double>();
            UnitOfMeasure = new List<string>();
            OriginalQuantities = new List<double>();
        }

        public void Menu()
        {
            int choice = 0;

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
                        switch (choice) // switch is a decent way to have a user choose the path of entry into the program, once guis are introduced, this can be cleaned up
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

            public void GetRecipe()
            {
                Console.WriteLine("What is the name of your recipe?");
                RecipeName = Console.ReadLine();

                while (string.IsNullOrEmpty(RecipeName))
                {
                    Console.WriteLine("Enter a recipe name");
                    RecipeName = Console.ReadLine();
                }

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
                string unitOfMeasure = Console.ReadLine();

                Ingredients.Add(ingredient);
                Quantities.Add(quantity);
                OriginalQuantities.Add(quantity);
                UnitOfMeasure.Add(unitOfMeasure);
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
                }
                else
                {
                    Console.WriteLine("Enter a recipe before trying to scale it");
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
                }
                else
                {
                    Console.WriteLine("\u001b[31mYour recipe was NOT cleared, enter 'yes' to clear\n\u001b[0m");
                }
            }
        }
    }
}