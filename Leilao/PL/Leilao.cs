using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leilao.PL
{
    public class Leilao
    {
        [Key]
        public int ID_Leilao { get; set; }
        [Required]
        [Display(Name ="Título do Leilão")]
        [StringLength(60, MinimumLength = 3)]
        [Column(TypeName = "varchar(100)")]
        public string Titulo { get; set; }
        [Column(TypeName = "varchar(500)")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        [Display(Name ="Natureza do Leilão")]
        [Column(TypeName = "varchar(30)")]
        public EnumNatureza Natureza { get; set; }
        [Required]
        [Display(Name = "Forma de Lance")]
        [Column(TypeName = "varchar(30)")]
        public EnumForma Forma { get; set; }
        [Required]
        [Display(Name = "Data de Início")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }
        [Required]
        [Display(Name = "Data de Encerramento")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime Termino { get; set; }
        [Required]
        [Display(Name = "Perfil Responsável")]
        public int FK_Responsavel { get; set; }
        public Usuario Usuario { get; set; }
        //public virtual Lote Lote { get; set; }
        public virtual ICollection<Lance> Lances { get; set; }
    }
}
