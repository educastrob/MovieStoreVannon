using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Data.Entities
{
    public class Locacao
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int FilmeId { get; set; }

        public DateTime DataLocacao { get; set; } = DateTime.Now;
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime? DataDevolucaoEfetiva { get; set; }

        [Range(0.01, 999.99)]
        public decimal Valor { get; set; }

        public decimal? ValorMulta { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }

        public bool Devolvido { get; set; } = false;

        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("FilmeId")]
        public virtual Filme? Filme { get; set; }
    }
}
