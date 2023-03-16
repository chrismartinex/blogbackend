using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
   return _context.BlogInfo.Where(item => item.userId == userId);

  }
  public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
  {
   return _context.BlogInfo.Where(item => item.category == category);
  }


  public IEnumerable<BlogItemModel> GetPublishedItems()
  {
   return _context.BlogInfo.Where(item => item.isPublished);
  }


  public List<BlogItemModel> GetItemsByTag(string Tag)
  {

   List<BlogItemModel> AllBlogWithTag = new List<BlogItemModel>();

   var allItems = GetAllBlogItems().Tolist();

   for (int i = 0; i < allItems.Count; i++)
   {

    BlogItemModel item = allItems[i];

    var itemArr = item.Tags.Split(",");

    for (int j = 0; j < itemArr.length; j++)
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
 }
}