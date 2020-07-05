using Leilao.BLL;
using Leilao.PL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leilao
{
    public class Facade
    {

        public IManager<Usuario> GetUsuarioManager()
        {
            return new UsuarioManager();
        }

        public IManager<PL.Leilao> GetLeilaoManager()
        {
            return new LeilaoManager();
        }

        public IManager<Lance> GetLanceManager()
        {
            return new LanceManager();
        }

        public IManager<Lote> GetLoteManager()
        {
            return new LoteManager();
        }

        public IManager<Produto> GetProdutoManager()
        {
            return new ProdutoManager();
        }

        public IManagerLeilao<PL.Leilao> GetLeilaoFunctions()
        {
            return new LeilaoFunctions();
        }
    }
}
