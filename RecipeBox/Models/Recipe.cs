using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
    {
        public Recipe()
        {
            this.JoinEntities = new HashSet<RecipeTag>();
        }

        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public string RecipeIngredients { get; set; }

        public string RecipeInstructions { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<RecipeTag> JoinEntities { get; set; }
    }
}