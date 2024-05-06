namespace POE_PART1
{
    class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public List<double> Quantities { get; set; }
        public List<string> UnitOfMeasure { get; set; }
        public List<double> Calories { get; set; }
        public List<string> FoodGroups { get; set; }
        public List<string> Steps { get; set; }
        public List<double> OriginalQuantities { get; set; }
        public List<double> OriginalCalories { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<string>();
            Quantities = new List<double>();
            UnitOfMeasure = new List<string>();
            OriginalQuantities = new List<double>();
            OriginalCalories = new List<double>();
            Calories = new List<double>();
            FoodGroups = new List<string>();
            Steps = new List<string>();
        }

        public void GetIngredients()
        {
            Console.WriteLine("Enter ingredients, quantities, units of measurement, calories, and food groups. Type 'done' to finish.");

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
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue; // need to store quanity in backup variable here, so that we can use the orignal value for resetting scale
                }

                Console.Write("Unit of Measure: ");
                string unitOfMeasure = Console.ReadLine();

                Console.Write("Calories: "); // Prompt for calories
                if (!double.TryParse(Console.ReadLine(), out double calories))
                {
                    Console.WriteLine("Invalid input for calories. Please enter a valid number.");
                    continue;
                }

                Console.Write("Food Group: "); // Prompt for food group
                string foodGroup = Console.ReadLine();

                Ingredients.Add(ingredient);
                Quantities.Add(quantity);
                OriginalQuantities.Add(quantity);
                UnitOfMeasure.Add(unitOfMeasure);
                Calories.Add(calories);
                FoodGroups.Add(foodGroup);
            }
        }

        public void GetSteps()
        {
            Console.WriteLine("Enter the number of steps in your recipe:");
            if (!int.TryParse(Console.ReadLine(), out int stepCount))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                Steps.Add(Console.ReadLine());
            }
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;

            for (int i = 0; i < Calories.Count; i++)
            {
                totalCalories += Calories[i]; // Add calorie value for each ingredient
            }

            return totalCalories;
        }

        public void Display()
        {
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
            Console.WriteLine($"Recipe Name: {Name}");

            // Check if all lists have the same count
            if (Ingredients.Count != Quantities.Count ||
                Ingredients.Count != UnitOfMeasure.Count ||
                Ingredients.Count != Calories.Count ||
                Ingredients.Count != FoodGroups.Count)
            {
                Console.WriteLine("Error: Mismatch in list lengths. Please check your recipe data.");
                return; // Exit if there's an inconsistency in list lengths
            }

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


        public void Scale(double factor)
        {
            // Make sure all lists are scaled consistently
            for (int i = 0; i < Ingredients.Count; i++)
            {
                if (i < Quantities.Count) Quantities[i] *= factor; // Scale quantity
                if (i < Calories.Count) Calories[i] *= factor; // Scale calories
            }
        }

        public void ResetToOriginal() // this method isnt working. values are not being reset
        {
            // Reset all related lists to their original values
            if (OriginalQuantities.Count == Quantities.Count)
            {
                Quantities = new List<double>(OriginalQuantities); // Reset quantities
            }

            if (OriginalCalories.Count == Calories.Count)
            {
                Calories = new List<double>(OriginalCalories); // Reset calories
            }
        }
    }
}
// bug - when i clicked clear recipe, i choose 1, yet option 2 was deleted. investigate
// add to the scale the option to reset back to original values
