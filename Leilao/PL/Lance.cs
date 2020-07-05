using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leilao.PL
{
    public class Lance
    {
        [Key]
        public int ID_Lance { get; set; }

        [Required]
        [Display(Name = "Leilão")]
        public int FK_Leilao { get; set; }
        public virtual Leilao Leilao { get; set; }

        [Required]
        [Display(Name = "Perfil")]
        public int FK_Usuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required]
        [Display(Name = "Valor do Lance")]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public double? Valor { get; set; }
        [Required]
        [Display(Name = "Data do Lance")]
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Column(TypeName = "char(1)")]
        [Display(Name = "Lance Vencedor")]
        public int FlagVencedor { get; set; }
    }
}