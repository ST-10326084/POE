# POE - Recipe Book Program - This is part 2 of 3. 
Welcome to the POE - Recipe Book Program, a C# application designed to help you store, manage, and scale your favorite recipes. This guide will walk you through the setup, usage, and features of the program.

Github Repository
Find the source code on GitHub: https://github.com/ST-10326084/POE/tree/Part2

Youtube Links
Watch a video of part 1 here: https://youtu.be/04h38jot0Ao

Watch a video of part 2 here: https://youtu.be/Kbjwl-Cvwo4

Overview
The POE Recipe Book Program allows you to:

Add new recipes with ingredients and preparation steps.
Display recipes with detailed ingredient lists and steps.
Scale recipes up or down and reset them to their original values.
Receive notifications when a recipe exceeds a specified calorie limit.
Clear specific recipes from the recipe book.
Instructions
Setup and Running the Program
Clone the Repository:

git clone https://github.com/ST-10326084/POE.git
cd POE
Build and Run the Program:
Ensure you have .NET SDK installed on your system. Build and run the program using the following commands:

dotnet build
dotnet run
Usage

Launching the Program:
Upon running the program, you'll be greeted with a welcome message and a menu of options.

Menu Options:
Enter a Recipe: Add a new recipe by providing its name, ingredients, quantities, units of measurement, calories, and food groups. You will also be prompted to enter the preparation steps.
Display a Recipe: View a list of all recipes and select one to display its detailed information.
Adjust the Scale of a Recipe: Scale a recipe by a specified factor (e.g., 0.5 for half, 2 for double). You can also reset the recipe to its original quantities and calories.
Clear a Specific Recipe: Remove a recipe from the recipe book after a confirmation prompt.
Exit: Exit the program.

Program Structure
The program consists of the following main classes:

Program: Entry point of the application, initializes and runs the recipe book menu.

RecipeBook: Manages the collection of recipes, handles user interactions, and provides methods to add, display, scale, and clear recipes.

Recipe: Represents a recipe, contains properties for ingredients and steps, and methods to calculate total calories, display the recipe, scale ingredients, and reset to original quantities.

Ingredient: Represents an ingredient with properties for name, quantity, unit of measure, calories, and food group.
