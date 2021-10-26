using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBox.Controllers
{
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public TagsController(RecipeBoxContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Tag.ToList());
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Tag tag)
    {
      bool isUnique = true;
      List<Tag> tagList = _db.Tag.ToList();
      foreach(Tag iteration in tagList)
      {
        if (tag.TagCategories == iteration.TagCategories)
        {
        isUnique = false;
        ModelState.AddModelError("DuplicateName", tag.TagCategories + " Is already taken");
        return View();
        }
      }
      if (isUnique)
      {
      _db.Tag.Add(tag);
      _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Tag thisTag = _db.Tag
          .Include(tag => tag.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

    public ActionResult Edit(int id)
    {
      Tag thisTag = _db.Tag.FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      Tag thisTag = _db.Tag.FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

//     [HttpPost]
//     public ActionResult AddEngineer(Machine machine, int EngineerId)
//     {
//       if (EngineerId != 0)
//       {
//       _db.License.Add(new License() { EngineerId = EngineerId, MachineId = machine.MachineId });
//       }

//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
//       return View(thisMachine);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
//       _db.Machines.Remove(thisMachine);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult DeleteEngineer(int joinId)
//     {
//       License joinEntry = _db.License.FirstOrDefault(entry => entry.LicenseId == joinId);
//       _db.License.Remove(joinEntry);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
  }
}