using Leilao.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Leilao
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new LeilaoContext();

            #region seeding
            if (db.Usuarios.Count() == 0)
            {
                Seed(db);
            }
            #endregion

        }
        private static void Seed(LeilaoContext context)
        {
            List<Usuario> usuarios = new List<Usuario>
            {
                new Usuario { Nome = "Josefredo", CpfOrCnpj = "99468739977"},
                new Usuario { Nome = "Lindomar", CpfOrCnpj = "00025465789"},
                new Usuario { Nome = "Regina", CpfOrCnpj = "65629585887" },
                new Usuario { Nome = "Maria", CpfOrCnpj = "96449898455"},
                new Usuario { Nome = "Betônia", CpfOrCnpj = "46468465168"},
                new Usuario { Nome = "Luis", CpfOrCnpj = "54151573011"},
                new Usuario { Nome = "Fernando", CpfOrCnpj = "26458145063"},
                new Usuario { Nome = "Augusto", CpfOrCnpj = "05217482087"},
                new Usuario { Nome = "Jose", CpfOrCnpj = "48383684029"},
                new Usuario { Nome = "Felipe", CpfOrCnpj = "11595976219009"}
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            List<PL.Leilao> leilaos = new List<PL.Leilao>
            {

                new PL.Leilao {Titulo = "Leilao01", Descricao = "Leilao de itens",
                    Natureza = EnumNatureza.Oferta,
                    Forma = EnumForma.Aberto,
                    Inicio = new DateTime(2020, 1, 1, 20,00,00),
                    Termino = new DateTime(2020, 1, 10, 10,00,00),
                    FK_Responsavel = usuarios.Single( p => p.Nome == "Betônia").ID_Usuario
                },
                new PL.Leilao {Titulo = "Leilao02", Descricao = "Leilao de carros",
                    Natureza = EnumNatureza.Demanda,
                    Forma = EnumForma.Fechado,
                    Inicio = new DateTime(2020, 1, 1, 20,00,00),
                    Termino = new DateTime(2020, 1, 15, 20,00,00),
                    FK_Responsavel = usuarios.Single( p => p.Nome == "Regina").ID_Usuario
                }
            };

            context.Leiloes.AddRange(leilaos);
            context.SaveChanges();

            List<Lote> lotes = new List<Lote>
                {
                new Lote {
                    ValorMaximo = 2000,
                    ValorMinimo = 500,
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao01").ID_Leilao,
                },

                new Lote {
                    ValorMaximo = 15000,
                    ValorMinimo = 4000,
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao02").ID_Leilao
                }

            };

            context.Lotes.AddRange(lotes);
            context.SaveChanges();

            List<Produto> produtos = new List<Produto>
            {
                new Produto {Categoria = EnumCategoria.Vestuário,
                    DescricaoCurta = "Tenis branco",
                    DescricaoLonga = "Tenis branco maneiro",
                    FK_Lote = 1
                },

                new Produto {Categoria = EnumCategoria.Vestuário,
                    DescricaoCurta = "Tenis azul",
                    DescricaoLonga = "Tenis azul maneiro",
                    FK_Lote = 1
                },

                new Produto {Categoria = EnumCategoria.Vestuário,
                    DescricaoCurta = "Calca amarela",
                    DescricaoLonga = "Calca amarela irada",
                    FK_Lote = 1
                },

                new Produto {Categoria = EnumCategoria.Eletrônico,
                    DescricaoCurta = "PS4",
                    DescricaoLonga = "PS4 usado",
                    FK_Lote = 1
                },

                new Produto {Categoria = EnumCategoria.Eletrônico,
                    DescricaoCurta = "PS3",
                    DescricaoLonga = "PS3 2 controles",
                    FK_Lote = 1
                },

                new Produto {Categoria = EnumCategoria.Eletrônico,
                    DescricaoCurta = "iphone 5",
                    DescricaoLonga = "iphone 5 todo quebrado",
                    FK_Lote = 1
                },
                new Produto {Categoria = EnumCategoria.Automobilístico,
                    DescricaoCurta = "fusca novo",
                    DescricaoLonga = "fusca 1980 todo revisado",
                    FK_Lote = 2
                },

                new Produto {Categoria = EnumCategoria.Automobilístico,
                    DescricaoCurta = "chevette desmontado",
                    DescricaoLonga = "chevette 1985 todo enferrujado",
                    FK_Lote = 2
                },

                new Produto {Categoria = EnumCategoria.Automobilístico,
                    DescricaoCurta = "BMW 320i",
                    DescricaoLonga = "bmw nova bem cara",
                    FK_Lote = 2
                },

                new Produto {Categoria = EnumCategoria.Automobilístico,
                    DescricaoCurta = "Fiat Palio",
                    DescricaoLonga = "Inteiro nada para fazer",
                    FK_Lote = 2 
                },
            };

            context.Produtos.AddRange(produtos);
            context.SaveChanges();

            List<Lance> lances = new List<Lance>
            {
                new Lance { 
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao01").ID_Leilao, 
                    FK_Usuario = usuarios.Single( p => p.Nome == "Felipe").ID_Usuario,
                    Valor = 1000,
                    Data = new DateTime(2020, 1, 2, 10,00,00),
                    FlagVencedor = '0'
                },

                new Lance {
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao01").ID_Leilao,
                    FK_Usuario = usuarios.Single( p => p.Nome == "Jose").ID_Usuario,
                    Valor = 1001,
                    Data = new DateTime(2020, 1, 2, 10,00,00),
                    FlagVencedor = '0'
                },

                new Lance {
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao02").ID_Leilao,
                    FK_Usuario = usuarios.Single( p => p.Nome == "Fernando").ID_Usuario,
                    Valor = 9500,
                    Data = new DateTime(2020, 1, 2, 10,00,00),
                    FlagVencedor = '0'
                },

                new Lance {
                    FK_Leilao = leilaos.Single( p => p.Titulo == "Leilao02").ID_Leilao,
                    FK_Usuario = usuarios.Single( p => p.Nome == "Luis").ID_Usuario,
                    Valor = 9700,
                    Data = new DateTime(2020, 1, 2, 10,00,00),
                    FlagVencedor = '0'
                }
            };

            context.Lances.AddRange(lances);
            context.SaveChanges();

        }
    }
}
