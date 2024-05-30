using System;
using System.Collections.Generic;
using System.Linq;

namespace POE_PART1
{
    class Program
    {
        private static void Main(string[] args)
        {
            RecipeBook recipeBook = new RecipeBook();

            // Subscribe to the OnHighCalories event
            recipeBook.OnHighCalories += message => Console.WriteLine(message); 

            recipeBook.Menu();

        }
    }
}