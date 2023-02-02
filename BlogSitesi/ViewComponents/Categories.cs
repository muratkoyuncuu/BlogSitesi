using BlogSitesi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly DatabaseContext _database;

        public Categories(DatabaseContext database)
        {
            _database = database;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _database.Categories.ToListAsync());
        }
    }
}
