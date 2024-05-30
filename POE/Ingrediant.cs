using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_PART1
{
    public class Ingredient // handles ingrediants, steps and scale
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, double quantity, string unitOfMeasure, double calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        public Ingredient Clone()
        {
            return new Ingredient(Name, Quantity, UnitOfMeasure, Calories, FoodGroup);
        }
    }
}
