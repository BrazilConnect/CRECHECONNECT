using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheConnect.Interfaces
{
    internal interface IDBRecord
    {
        string TableName { get; }
        int Id { get; }
        string Cpf { get; }
        string NomeCompleto { get; }
        DateTime DataNascimento { get; }
        Enumeradores.Genero Genero { get; }
        void Delete();

        void Update();

        List<Aluno> Read();
    }
}
