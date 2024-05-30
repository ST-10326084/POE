namespace POE_PART1
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        private List<Ingredient> OriginalIngredients { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            OriginalIngredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            OriginalIngredients.Add(ingredient.Clone());
        }

        public void GetIngredients()
        {
            Console.WriteLine("Enter ingredients, quantities, units of measurement, calories, and food groups. Type 'done' to finish.");

            while (true)
            {
                Console.Write("Ingredient: ");
                string ingredientName = Console.ReadLine();
                if (string.Equals(ingredientName, "done", StringComparison.OrdinalIgnoreCase)) break;

                double quantity;
                while (true)
                {
                    Console.Write("Quantity: ");
                    if (double.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please enter a valid number greater than 0.");
                    }
                }

                Console.Write("Unit of Measure: ");
                string unitOfMeasure = Console.ReadLine();

                double calories;
                while (true)
                {
                    Console.Write("Calories: ");
                    if (double.TryParse(Console.ReadLine(), out calories) && calories >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid calories. Please enter a valid number greater than or equal to 0.");
                    }
                }

                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                AddIngredient(new Ingredient(ingredientName, quantity, unitOfMeasure, calories, foodGroup));
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
            return Ingredients.Sum(ingredient => ingredient.Calories);
        }

        public void Display()
        {
            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
            Console.WriteLine($"Recipe Name: {Name}");

            Console.WriteLine("Ingredients:");
            Console.WriteLine("Name\t\t\tQuantity\tUnit\t\tCalories\tFood Group");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Name}\t\t{ingredient.Quantity}\t\t{ingredient.UnitOfMeasure}\t\t{ingredient.Calories}\t\t{ingredient.FoodGroup}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {Steps[i]}");
            }

            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories()}");

            Console.WriteLine("\x1b[34m-------------------------------\x1b[0m");
        }

        public void Scale(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
                ingredient.Calories *= factor;
            }
        }

        public void ResetToOriginal()
        {
            Ingredients = OriginalIngredients.Select(ingredient => ingredient.Clone()).ToList();
        }
    }
}

