using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;
using MovieStore.Data.Repositories;
using MovieStore.Services.Validators;

namespace MovieStore.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _usuarioRepository;

        public UsuarioService(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            if (!ValidationHelper.IsValidEmail(usuario.Email))
            {
                throw new ArgumentException("Email inválido. Por favor, insira um email válido.");
            }

            usuario.DataCriacao = DateTime.Now;
            usuario.Ativo = true;
            return await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
