using MediaStore.Core.Models;
using MediaStore.WebAPI.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    //private static List<Book> list = new List<Book>();
    private readonly BackendDataContext _context;

    public BooksController(BackendDataContext context)
    {
      _context = context;
    }

    //--------
    // CRUD
    //--------
    // C
    [HttpPost]
    public ActionResult Create([FromBody] Book book)
    {
      _context.DbSetBooks.Add(book);
      _context.SaveChanges();
      return Ok($"Created {book.Id}");
    }
    // R
    [HttpGet("{id}")]
    public ActionResult Read(int id)
    {
      Book? book = _context.DbSetBooks.Find(id);
      if (book == null)
      {
        return BadRequest($"Not found: {id}");
      }
      return Ok(book);
    }
    // R
    [HttpGet]
    public ActionResult ReadAll()
    {
      List<Book> list = _context.DbSetBooks.ToList<Book>();
      if (list == null || list.Count == 0)
      {
        return NotFound("No data, nothing to read.");
      }
      return Ok(list);
    }
    // U
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] Book book)
    {
      _context.DbSetBooks.Update(book);
      _context.SaveChanges();
      return Ok($"Updated {id}");
    }
    // D
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      Book? book = _context.DbSetBooks.Find(id);
      if (book == null)
      {
        return BadRequest($"Not found: {id}");
      }
      _context.DbSetBooks.Remove(book);
      _context.SaveChanges();

      return Ok($"Deleted {id}");
    }
    // D
    [HttpDelete]
    public ActionResult DeleteAll()
    {
      List<Book> list = _context.DbSetBooks.ToList<Book>();

      foreach (Book x in list)
      {
        _context.DbSetBooks.Remove(x);
        _context.SaveChanges();
      }
      return Ok($"Deleted ALL");
    }
    //----------
    // internal
    // not needed anymore 
    //
    //private int GetNextId()
    //{
    //  if (list.Count == 0) return 1; 

    //  List<int> ids = new List<int>();
    //  foreach (Book b in list) { ids.Add(b.Id); }
    //  ids.Sort();

    //  return (ids[ids.Count - 1] + 1); 
    //}
    //private int FindById(int id)
    //{
    //  for (int i = 0;  i < list.Count; i++) { 
    //    if (list[i].Id == id) return i; 
    //  }
    //  return -1;  
    //}
  }
}