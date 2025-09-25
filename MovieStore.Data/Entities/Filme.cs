using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Entities
{
    public class Filme
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Gênero é obrigatório")]
        [StringLength(50)]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Diretor é obrigatório")]
        [StringLength(100)]
        public string Diretor { get; set; } = string.Empty;

        public int AnoLancamento { get; set; }
        public int DuracaoMinutos { get; set; }

        [Range(0.01, 999.99)]
        public decimal ValorLocacao { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantidadeDisponivel { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}
