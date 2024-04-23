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

            do
            {
                Console.WriteLine("Welcome to DoorNo's, the best recipe storage app ever made");
                Console.WriteLine("Enter 1 to Enter a recipe");
                Console.WriteLine("Enter 2 to Display a recipe");
                Console.WriteLine("Enter 3 to Adjust the scale of the recipe");
                Console.WriteLine("Enter 4 to Clear all data and enter a new recipe");
                Console.WriteLine("Enter 9 to Exit");

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

        public void DisplayRecipe()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Recipe Name: {RecipeName}");
            Console.WriteLine("Ingredients:");

            for (int i = 0; i < Ingredients.Count; i++)
            {
                Console.WriteLine($"{Quantities[i]} {UnitOfMeasure[i]} of {Ingredients[i]}");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {Steps[i]}");
            }
            Console.WriteLine("-------------------------------");
        }

        public void ScaleRecipe()
        {
            Console.WriteLine("Enter a scale factor to adjust the recipe:");
            if (!double.TryParse(Console.ReadLine(), out double factor))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            for (int i = 0; i < Quantities.Count; i++)
            {
                Quantities[i] *= factor;
            }
        }

        public void ClearRecipe()
        {
            Console.WriteLine("Are you sure you want to clear the recipe? Type 'yes' to confirm.");

            if (string.Equals(Console.ReadLine(), "yes", StringComparison.OrdinalIgnoreCase))
            {
                RecipeName = string.Empty;
                Ingredients.Clear();
                Steps.Clear();
                Quantities.Clear();
                UnitOfMeasure.Clear();
                OriginalQuantities.Clear();
                recipeEntered = false;

                Console.WriteLine("Recipe cleared.");
            }
            else
            {
                Console.WriteLine("Recipe not cleared.");
            }
        }
    }
}