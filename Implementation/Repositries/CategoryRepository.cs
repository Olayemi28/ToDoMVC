using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Implementation.Repositries
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Categories.AnyAsync(d => d.Id == id && d.IsDeleted == false);
        }

        public async Task<bool> ExistsByName(string name)
        {
           return await _context.Categories.AnyAsync(s => s.Name.Equals(name) && s.IsDeleted == false);
        }

        public async Task<Category> GetByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(s => s.Name.Equals(name) && s.IsDeleted == false);
        }

        public async Task<IEnumerable<Category>> GetSelectedCategories(IList<int> ids)
        {
            return await _context.Categories.Where(d => ids.Contains(d.Id)).ToListAsync();
        }
    }
}