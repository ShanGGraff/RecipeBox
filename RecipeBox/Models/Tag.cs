using System.Collections.Generic;
using System;

namespace RecipeBox.Models
{
  public class Tag
  {
    public Tag()
    {
      this.JoinEntities = new HashSet<RecipeTag>();
    }

    public int TagId { get; set; }
    public string TagCategories { get; set; }

    public virtual ICollection<RecipeTag> JoinEntities { get;}
  }
}