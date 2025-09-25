using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14)]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        [StringLength(200)]
        public string Endereco { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}
