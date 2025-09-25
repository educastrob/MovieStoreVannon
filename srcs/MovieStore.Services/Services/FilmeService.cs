using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;
using MovieStore.Data.Repositories;

namespace MovieStore.Services.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IRepository<Filme> _filmeRepository;

        public FilmeService(IRepository<Filme> filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<IEnumerable<Filme>> GetAllFilmesAsync()
        {
            return await _filmeRepository.GetAllAsync();
        }

        public async Task<Filme?> GetFilmeByIdAsync(int id)
        {
            return await _filmeRepository.GetByIdAsync(id);
        }

        public async Task<Filme> CreateFilmeAsync(Filme filme)
        {
            filme.DataCadastro = DateTime.Now;
            filme.Ativo = true;
            return await _filmeRepository.AddAsync(filme);
        }

        public async Task<Filme> UpdateFilmeAsync(Filme filme)
        {
            return await _filmeRepository.UpdateAsync(filme);
        }

        public async Task DeleteFilmeAsync(int id)
        {
            await _filmeRepository.DeleteAsync(id);
        }
    }
}
