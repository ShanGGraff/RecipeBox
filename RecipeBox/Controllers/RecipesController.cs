using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBox.Controllers
{
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;

    public RecipesController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Recipe.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe)
    {
      bool isUnique = true;
      List<Recipe> recipeList = _db.Recipe.ToList();
      foreach(Recipe iteration in recipeList)
      {
        if (recipe.RecipeName == iteration.RecipeName)
        {
        isUnique = false;
        ModelState.AddModelError("DuplicateName", recipe.RecipeName + " Is already hired");
        return View();
        }
      }
      if (isUnique)
      {
      _db.Recipe.Add(recipe);
      _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

//     public ActionResult Details(int id)
//     {
//       Engineer thisEngineer = _db.Engineers
//           .Include(engineer => engineer.JoinEntities)
//           .ThenInclude(join => join.Machine)
//           .FirstOrDefault(engineer => engineer.EngineerId == id);
//       return View(thisEngineer);
//     }

//     public ActionResult Edit(int id)
//     {
//       Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
//       return View(thisEngineer);
//     }

//     [HttpPost]
//     public ActionResult Edit(Engineer engineer, int MachineId)
//     {

//       _db.Entry(engineer).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//     public ActionResult AddMachine(int id)
//     {
//       Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
//       ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
//       return View(thisEngineer);
//     }

//     [HttpPost]
//     public ActionResult AddMachine(Engineer engineer, int MachineId)
//     {
//       if (MachineId != 0)
//       {
//       _db.License.Add(new License() { MachineId = MachineId, EngineerId = engineer.EngineerId });
//       }

//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
//       return View(thisEngineer);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
//       _db.Engineers.Remove(thisEngineer);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//     public ActionResult DeleteMachine(int joinId)
//     {
//       License joinEntry = _db.License.FirstOrDefault(entry => entry.LicenseId == joinId);
//       _db.License.Remove(joinEntry);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
  }
}