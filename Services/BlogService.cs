using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogbackend.Models;
using blogbackend.Services.Context;

namespace blogbackend.Services
{
 public class BlogService
 {
  private readonly DataContext _context;

  public BlogService(DataContext context)
  {
   _context = context;
  }
  public bool AddBlogItem(BlogItemModel newBlogItem)
  {
   _context.Add(newBlogItem);
   return _context.SaveChanges() != 0;

  }
  public IEnumerable<BlogItemModel> GetAllBlogItems()
  {
   return _context.BlogInfo;
  }

  public IEnumerable<BlogItemModel> GetItemsByUserId(int userId)
  {
   return _context.BlogInfo.Where(item => item.userID == userId);

  }
  public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
  {
   return _context.BlogInfo.Where(item => item.Category == category);
  }

  public IEnumerable<BlogItemModel> GetItemsByDate(string Date)
  {
   return _context.BlogInfo.Where(item => item.Date == Date);
  }


  public IEnumerable<BlogItemModel> GetPublishedItems()
  {
   return _context.BlogInfo.Where(item => item.isPublished);
  }


  public List<BlogItemModel> GetItemsByTag(string Tag)
  {

   List<BlogItemModel> AllBlogWithTag = new List<BlogItemModel>();

   var allItems = GetAllBlogItems().ToList();

   for (int i = 0; i < allItems.Count; i++)
   {

    BlogItemModel item = allItems[i];

    var itemArr = item.Tags.Split(",");

    for (int j = 0; j < itemArr.Length; j++)
    {
     //checking if item array has the tag were looking for
     if (itemArr[j].Contains(Tag))
     {
      AllBlogWithTag.Add(item);
     }
    }

   }
   return AllBlogWithTag;

  }
  public bool UpdateBlogItem(BlogItemModel BlogUpdate){
   _context.Update<BlogItemModel>(BlogUpdate);
   return _context.SaveChanges() !=0;
  }
  public bool DeleteBlogItem(BlogItemModel BlogDelete){
   BlogDelete.isDeleted = true;
   _context.Update<BlogItemModel>(BlogDelete);
   return _context.SaveChanges() != 0;
  }
 public BlogItemModel GetBlogItemById(int id){
  return _context.BlogInfo.SingleOrDefault(item => item.id == id);
 }


 }
}