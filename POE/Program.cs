namespace POE_PART1 
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // recipe program, where you can track recipes and ingrediants
            // note (only 1 ) recipe
            // each step in creating the recipe,
            // use arrays to store steps in the recipe and ingrediants
            // must be able to clear the recipe, and add a new one
            // scale and reset quantiies of ingrediants
            // should not store data, only use memory (no .txt) arrays 

            //recipe recipe new Recipe(); // create object to reference static 
            Recipe recipe = new Recipe();
            recipe.getRecipe();
        }

    }
    class Recipe
    {
        public Recipe()
        {
            // contructor
        }

        public void getRecipe()
        {
            Console.WriteLine("What is the name of your recipe?");
            String recipeName = Console.ReadLine();

            Console.WriteLine("List the ingridients seperated by a ,");
            String allIngriedints = Console.ReadLine();

        }
    }
}
      


