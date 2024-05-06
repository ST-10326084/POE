namespace POE_PART1
{
	class RecipeBook
	{
		// Delegate and event for calorie notifications
		public delegate void CalorieNotificationHandler(string message);
		public event CalorieNotificationHandler OnHighCalories;

		// Store multiple recipes in a list
		private List<Recipe> recipes;

		public RecipeBook()
		{
			recipes = new List<Recipe>();

			InitializePredefinedRecipes();
		}
		private void InitializePredefinedRecipes()
		{
			// Hardcode a couple of recipes into the system

			// First Recipe: Spaghetti Bolognese
			var spaghettiBolognese = new Recipe("Spaghetti Bolognese");
			spaghettiBolognese.Ingredients.Add("Spaghetti");
			spaghettiBolognese.Quantities.Add(200);
			spaghettiBolognese.UnitOfMeasure.Add("grams");
			spaghettiBolognese.Calories.Add(350);
			spaghettiBolognese.FoodGroups.Add("Carbohydrates");

			spaghettiBolognese.Ingredients.Add("Ground Beef");
			spaghettiBolognese.Quantities.Add(250);
			spaghettiBolognese.UnitOfMeasure.Add("grams");
			spaghettiBolognese.Calories.Add(250);
			spaghettiBolognese.FoodGroups.Add("Proteins");

			spaghettiBolognese.Steps.Add("Boil spaghetti until al dente.");
			spaghettiBolognese.Steps.Add("Cook ground beef with seasoning.");
			spaghettiBolognese.Steps.Add("Mix spaghetti with beef and tomato sauce.");
			spaghettiBolognese.Steps.Add("Serve with grated cheese.");

			recipes.Add(spaghettiBolognese);

			// Second Recipe: Caesar Salad
			var caesarSalad = new Recipe("Caesar Salad");
			caesarSalad.Ingredients.Add("Romaine Lettuce");
			caesarSalad.Quantities.Add(100);
			caesarSalad.UnitOfMeasure.Add("grams");
			caesarSalad.Calories.Add(20);
			caesarSalad.FoodGroups.Add("Vegetables");

			caesarSalad.Ingredients.Add("Caesar Dressing");
			caesarSalad.Quantities.Add(50);
			caesarSalad.UnitOfMeasure.Add("grams");
			caesarSalad.Calories.Add(180);
			caesarSalad.FoodGroups.Add("Fats");

			caesarSalad.Steps.Add("Tear lettuce into pieces.");
			caesarSalad.Steps.Add("Add Caesar dressing and mix.");
			caesarSalad.Steps.Add("Add croutons and Parmesan cheese.");
			caesarSalad.Steps.Add("Serve chilled.");

			recipes.Add(caesarSalad);
		}

		public void Menu()
		{
			int choice = 0;

			do
			{
				Console.WriteLine("\x1b[34m\nWelcome to DoorNo's, the best recipe storage app ever made\x1b[0m");
				Console.WriteLine("Our goal is to help you stop spending your hard-earned money on takeaways, and teach you how to cook.\n");

				Console.WriteLine("Enter 1 to Enter a recipe\n" +
								  "Enter 2 to Display a recipe\n" +
								  "Enter 3 to Adjust the scale of a recipe\n" +
								  "Enter 4 to Clear a specific recipe\n" +
								  "Enter 9 to Exit");

				try
				{
					choice = Convert.ToInt32(Console.ReadLine());

					switch (choice)
					{
						case 1:
							AddRecipe();
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

		private void AddRecipe()
		{
			Console.WriteLine("Enter the recipe name:");
			string recipeName = Console.ReadLine();

			if (string.IsNullOrEmpty(recipeName))
			{
				Console.WriteLine("Recipe name cannot be empty.");
				return;
			}

			// Check for duplicate recipe names
			if (recipes.Any(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase)))
			{
				Console.WriteLine($"The recipe '{recipeName}' already exists. Choose a different name.");
				return;
			}

			var newRecipe = new Recipe(recipeName);
			newRecipe.GetIngredients();
			newRecipe.GetSteps();

			recipes.Add(newRecipe);

			Console.WriteLine($"Recipe '{recipeName}' added successfully.");
		}

		private void DisplayRecipe()
		{
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes available.");
				return;
			}

			// Sort recipes alphabetically by name
			var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

			// Display all available recipe names for selection
			Console.WriteLine("Available Recipes:");
			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				Console.WriteLine($"{i + 1}: {sortedRecipes[i].Name}");
			}

			Console.Write("Select a recipe to display (enter the corresponding number): ");
			if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex < 1 || recipeIndex > sortedRecipes.Count)
			{
				Console.WriteLine("Invalid selection. Please enter a valid number.");
				return;
			}

			// Retrieve the selected recipe
			var selectedRecipe = sortedRecipes[recipeIndex - 1];

			// Calculate total calories for the selected recipe
			double totalCalories = selectedRecipe.CalculateTotalCalories();

			if (totalCalories > 300) // If total calories exceed 300, trigger the event
			{
				OnHighCalories?.Invoke($"\x1b[31mWarning: Recipe '{selectedRecipe.Name}' exceeds 300 calories with a total of {totalCalories} calories.\x1b[0m");

			}

			selectedRecipe.Display(); // Display the recipe
		}

		private void ScaleRecipe()
		{
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes available to scale.");
				return;
			}

			// Sort recipes and ask which one to scale
			var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

			Console.WriteLine("Available Recipes:");
			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				Console.WriteLine($"{i + 1}: {sortedRecipes[i].Name}");
			}

			Console.Write("Select a recipe to scale (enter the corresponding number): ");
			if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex < 1 || recipeIndex > sortedRecipes.Count)
			{
				Console.WriteLine("Invalid selection. Please enter a valid number.");
				return;
			}

			var selectedRecipe = sortedRecipes[recipeIndex - 1]; // Adjusted for 0-based index

			Console.WriteLine("Enter the scale factor (e.g., 0.5 for half scale, 2 for double scale, 1 to reset to original quantities):");
			if (!double.TryParse(Console.ReadLine(), out double scaleFactor))
			{
				Console.WriteLine("Invalid input. Please enter a valid number.");
				return;
			}

			if (scaleFactor == 1) // Reset to original values
			{
				selectedRecipe.ResetToOriginal(); // Reset method to restore original quantities and calories
				Console.WriteLine($"Recipe '{selectedRecipe.Name}' has been reset to original values.");
			}
			else
			{
				selectedRecipe.Scale(scaleFactor); // Scale with given factor
				Console.WriteLine($"Recipe '{selectedRecipe.Name}' has been scaled by a factor of {scaleFactor}.");
			}
		}

		private void ClearRecipe()
		{
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes available to clear.");
				return;
			}

			// Sort recipes and ask which one to clear
			var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();

			Console.WriteLine("Available Recipes:");
			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				Console.WriteLine($"{i + 1}: {sortedRecipes[i].Name}");
			}

			Console.Write("Select a recipe to clear (enter the corresponding number): ");
			if (!int.TryParse(Console.ReadLine(), out int recipeIndex) || recipeIndex < 1 || recipeIndex > sortedRecipes.Count)
			{
				Console.WriteLine("Invalid selection. Please enter a valid number.");
				return;
			}


			var recipeToDelete = sortedRecipes[recipeIndex - 1];

			recipes.Remove(recipeToDelete);

			Console.WriteLine($"Recipe '{recipeToDelete.Name}' cleared successfully.");
		}
	}
}
