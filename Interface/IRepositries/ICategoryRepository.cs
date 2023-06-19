using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> ExistsByName(string name);
        
        Task<bool> ExistsById(int id);

        Task<Category> GetByName(string name);

        Task<IEnumerable<Category>> GetSelectedCategories(IList<int> ids);
    }
}