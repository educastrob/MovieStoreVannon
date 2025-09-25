using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;

namespace MovieStore.Services.Services
{
    public interface ILocacaoService
    {
        Task<IEnumerable<Locacao>> GetAllLocacoesAsync();
        Task<Locacao?> GetLocacaoByIdAsync(int id);
        Task<Locacao> CreateLocacaoAsync(Locacao locacao);
        Task<Locacao> UpdateLocacaoAsync(Locacao locacao);
        Task DeleteLocacaoAsync(int id);
    }
}
