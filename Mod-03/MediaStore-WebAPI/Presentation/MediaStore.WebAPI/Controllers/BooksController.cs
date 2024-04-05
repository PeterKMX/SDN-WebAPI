using MediaStore.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private static List<Book> list = new List<Book>();

    // ----
    // CRUD
    [HttpPost]
    public string Create([FromBody] Book b)
    {
      b.Id = GetNextId();
      list.Add(b);
      return $"Created {b.Id}";
    }
    [HttpGet("{id}")]
    public Book Read(int id)
    {
      int i = FindById(id);
      return list[i];
    }
    [HttpGet]
    public List<Book> ReadAll()
    {
      return list;
    }
    [HttpPut("{id}")]
    public string Update(int id, [FromBody] Book b)
    {
      int i = FindById(id);
      list[i] = b;
      return $"Updated {id}";
    }
    [HttpDelete("{id}")]
    public string Delete(int id)
    {
      int i = FindById(id);
      list.RemoveAt(i);
      return $"Deleted {id}";
    }
    // -------
    // helpers
    private int GetNextId()
    {
      if (list.Count == 0) 
      { 
        return 1;       
      }

      List<int> ids = new List<int>();
      foreach (Book b in list) { ids.Add(b.Id); }
      ids.Sort();
      return (ids[ids.Count - 1] + 1);
    }
    private int FindById(int id)
    {
      for (int i = 0; i < list.Count; i++)
      {
        if (list[i].Id == id) return i;
      }
      return -1;
    }
  }
}

