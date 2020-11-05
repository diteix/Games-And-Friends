using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesAndFriends.Domain.Repository
{
    public interface ICrudRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<T> UpdateAsync(int id, T entity);
    }
}