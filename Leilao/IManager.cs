using System;
using System.Collections.Generic;
using System.Text;

namespace Leilao
{
    public interface IManager<T>
    {
        T Create(T t);
        T Read(int id);
        List<T> Read();
        T Update(T t);
        bool Delete(int id);
        bool Exists(int id);
        List<T> ReadCurrent(string v);
        bool VerificaSePodeEditar(string login, int id);
    }
}
