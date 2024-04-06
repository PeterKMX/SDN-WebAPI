using MediaStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.WebAPI.DataAccess
{
  public class BackendDataContext: DbContext
  {
    public DbSet<Book> DbSetBooks { get; set; }

    public BackendDataContext(DbContextOptions<BackendDataContext> options)
    : base(options)
    { }

  }
}
