using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Data.Entities;
using MovieStore.Data.Repositories;
using MovieStore.Services.Validators;

namespace MovieStore.Services.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<Cliente> _clienteRepository;

        public ClienteService(IRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            if (!ValidationHelper.IsValidEmail(cliente.Email))
            {
                throw new ArgumentException("Email inválido. Por favor, insira um email válido.");
            }
            
            if (!ValidationHelper.IsValidCPF(cliente.CPF))
            {
                throw new ArgumentException("CPF inválido. Por favor, insira um CPF válido.");
            }
            
            if (!string.IsNullOrWhiteSpace(cliente.Telefone) && !ValidationHelper.IsValidPhone(cliente.Telefone))
            {
                throw new ArgumentException("Telefone inválido. Por favor, insira um telefone válido no formato (XX) XXXXX-XXXX.");
            }
            
            cliente.CPF = ValidationHelper.FormatCPF(cliente.CPF);
            if (!string.IsNullOrWhiteSpace(cliente.Telefone))
            {
                cliente.Telefone = ValidationHelper.FormatPhone(cliente.Telefone);
            }
            
            if (cliente.DataNascimento == DateTime.MinValue)
            {
                cliente.DataNascimento = new DateTime(1990, 1, 1);
            }

            cliente.DataCadastro = DateTime.Now;
            cliente.Ativo = true;
            return await _clienteRepository.AddAsync(cliente);
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            return await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteClienteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }
    }
}
