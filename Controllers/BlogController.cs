using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blogbackend.Controllers
{
 [ApiController]
 [Route("[controller]")]
 public class BlogController : ControllerBase
 {
  private readonly BlogService _data;
  public BlogController(BlogService dataFromService)
  {
   _data = dataFromService;
  }

  [HttpPost]
  [Route("AddBlogItem")]
  public bool AddBlogItem(BlogItemModel newBlogItem) {
            return _data.AddBlogItem(newBlogItem);

        }

 [HttpGet]
 [Get("GetAllBlogItems")]
 public IEnumerable<GetItemModel> GetAllBlogItems()
 {
  return_data.GetAllBlogItems();
 }

 [HttpGet]
 [Route("GetItemsByUSerId/(userId")]
 public IEnumerable<BlogItemModel> GetItemsByUserId(int userId)
 {
  return _data.GetItemsByUserIdUserId(userId);
 }

 [HttpGet]
 [Route("GetItemsByCategory/{category}")]
 public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
 {
  return _data.GetItemsByCategory(category);
 }

 [HttpGet]
 [Route("GetItemsByDate/{date}")]

 public IEnumerable<BlogItemModel> GetItemsByDate(string date)
 {
  return _data.GetItemsByDate(date);
 }

 [HttpGet]
 [Route("GetPublishedItems")]

 public IEnumerable<BlogItemModel> GetPublishedItems()
 {
  return _data.GetPublishedItems();
 }

 [HttpGet]
 [Route("GetItemsBytag/{Tag}")]
 public List<BlogItemModel> GetItemsByTag(string Tag)
 {
  return _data.GetItemsByTag(Tag);
 }

}
}