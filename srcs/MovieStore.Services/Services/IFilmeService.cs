using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;

namespace MovieStore.Services.Services
{
    public interface IFilmeService
    {
        Task<IEnumerable<Filme>> GetAllFilmesAsync();
        Task<Filme?> GetFilmeByIdAsync(int id);
        Task<Filme> CreateFilmeAsync(Filme filme);
        Task<Filme> UpdateFilmeAsync(Filme filme);
        Task DeleteFilmeAsync(int id);
    }
}
