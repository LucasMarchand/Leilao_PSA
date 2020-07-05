using System.Collections.Generic;

namespace Leilao
{
    public interface IManagerLeilao<T>
    {
        List<T> ReadLeiloesParaDarLance(string login);
    }
}