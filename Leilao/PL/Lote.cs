using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leilao.PL
{
    public class Lote
    {
        [Key]
        public int ID_Lote { get; set; }
        [Required]
        [Display(Name = "Leilão")]
        public int FK_Leilao { get; set; }
        public Leilao Leilao { get; set; }
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Valor Mínimo")]
        public double? ValorMinimo { get; set; }
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Valor Máximo")]
        public double? ValorMaximo { get; set; }
    
        public ICollection<Produto> Produtos { get; set; }

    }
}