using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;
using MovieStore.Data.Repositories;

namespace MovieStore.Services.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly IRepository<Locacao> _locacaoRepository;

        public LocacaoService(IRepository<Locacao> locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        public async Task<IEnumerable<Locacao>> GetAllLocacoesAsync()
        {
            return await _locacaoRepository.GetAllAsync();
        }

        public async Task<Locacao?> GetLocacaoByIdAsync(int id)
        {
            return await _locacaoRepository.GetByIdAsync(id);
        }

        public async Task<Locacao> CreateLocacaoAsync(Locacao locacao)
        {
            if (locacao.ClienteId <= 0)
            {
                throw new ArgumentException("Cliente é obrigatório para realizar a locação.");
            }

            if (locacao.FilmeId <= 0)
            {
                throw new ArgumentException("Filme é obrigatório para realizar a locação.");
            }

            if (locacao.DataDevolucaoPrevista <= DateTime.Now.Date)
            {
                throw new ArgumentException("Data de devolução deve ser maior que a data atual.");
            }
            
            locacao.DataLocacao = DateTime.Now;
            locacao.Devolvido = false;

            return await _locacaoRepository.AddAsync(locacao);
        }

        public async Task<Locacao> UpdateLocacaoAsync(Locacao locacao)
        {
            return await _locacaoRepository.UpdateAsync(locacao);
        }

        public async Task DeleteLocacaoAsync(int id)
        {
            await _locacaoRepository.DeleteAsync(id);
        }
    }
}
