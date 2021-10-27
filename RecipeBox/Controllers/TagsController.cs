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
    public ActionResult AddRecipe(int id)
    {
      Tag thisTag = _db.Tag.FirstOrDefault(tag => tag.TagId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipe, "RecipeId", "RecipeName");
      return View(thisTag);
    }
    
    [HttpPost]
    public ActionResult AddRecipe(Tag tag, int RecipeId)
      {
        if (RecipeId != 0)
        {
        _db.RecipeTag.Add(new RecipeTag() { RecipeId = RecipeId, TagId = tag.TagId });
        }

        _db.SaveChanges();
        return RedirectToAction("Index");
      }

    public ActionResult Delete(int id)
    {
      Tag thisTag = _db.Tag.FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Tag thisTag = _db.Tag.FirstOrDefault(tag => tag.TagId == id);
      _db.Tag.Remove(thisTag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult DeleteRecipe(int joinId)
    {
      RecipeTag joinEntry = _db.RecipeTag.FirstOrDefault(entry => entry.RecipeTagId == joinId);
      _db.RecipeTag.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}