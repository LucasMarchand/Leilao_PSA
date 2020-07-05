using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leilao.PL
{
    public enum EnumCategoria
    {
        Automobilístico,
        Vestuário,
        Mobiliário,
        Imobiliário,
        Informática,
        Eletrônico
    }

    public class Produto
    {
        [Key]
        public int ID_Produto { get; set; }

        [Required]
        [Display(Name = "Lote")]
        public int FK_Lote { get; set; }
        public Lote Lote { get; set; }

        [Required]
        [Display(Name = "Descrição Curta")]
        [Column(TypeName = "varchar(100)")]
        public string DescricaoCurta { get; set; }
        
        [Required]
        [Display(Name ="Descrição Longa")]
        [Column(TypeName = "varchar(1000)")]
        public string DescricaoLonga { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public EnumCategoria Categoria { get; set; }
        //public byte[] Foto { get; set; }
    }
}