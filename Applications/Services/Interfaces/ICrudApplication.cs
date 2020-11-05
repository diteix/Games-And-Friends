using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesAndFriends.Application.Services.Interfaces 
{
    public interface ICrudApplication<T>
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<T> UpdateAsync(int id, T entity);
    }
}