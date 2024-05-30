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

                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit of Measure: ");
                string unitOfMeasure = Console.ReadLine();

                Console.Write("Calories: ");
                double calories = double.Parse(Console.ReadLine());

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
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitOfMeasure} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup} food group)");
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {Steps[i]}");
            }

            Console.WriteLine($"Total Calories: {CalculateTotalCalories()}");

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
// add to the scale the option to reset back to original values
