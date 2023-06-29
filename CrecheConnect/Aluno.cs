using CrecheConnect.BancoDeDados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CrecheConnect
{
    internal class Aluno : DBRecordBase
    {
        public override string TableName => "ALUNOS";
        public override int Id { get; }
        public override string Cpf { get; set; }
        public override string NomeCompleto { get; set; }
        public override DateTime DataNascimento { get; set; }
        public override Enumeradores.Genero Genero { get; set; }

        internal Aluno(bool createByConsole)
        {
            if (createByConsole)
            {
                Console.Write("CPF: ");
                Cpf = Console.ReadLine();
                Console.Write("Nome completo: ");
                NomeCompleto = Console.ReadLine();
                Console.Write("Data de nascimento [DD/MM/AAAA]: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
                {
                    DataNascimento = dataNascimento;
                }
                else
                {
                    Console.Write("Opção inválida!");
                }
                Console.Write("Gênero [F/M]: ");
                Genero = (Enumeradores.Genero)Enum.Parse(typeof(Enumeradores.Genero), Console.ReadLine().ToUpper());
            }
        }

        internal Aluno(long id)
        {
            using (var conn = new SqlConnection(DBInfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT ID, CPF, NOMECOMPLETO, DATANASCIMENTO, GENERO FROM ALUNOS WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        Cpf = reader.GetString(1);
                        NomeCompleto = reader.GetString(2);
                        DataNascimento = reader.GetDateTime(3);
                        Genero = (Enumeradores.Genero)Enum.ToObject(typeof(Enumeradores.Genero), reader.GetInt32(4));
                    }
                }
            }
        }

        internal Aluno(int id, string cpf, string nomeCompleto, DateTime dataNascimento, Enumeradores.Genero genero)
        {
            Id = id;
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Genero = genero;
        }

        internal bool IsValid()
        {
            return Id > 0;
        }

        internal void Save()
        {
            using (var conn = new SqlConnection(DBInfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO ALUNOS (CPF, NOMECOMPLETO, DATANASCIMENTO, GENERO) VALUES (@CPF, @NOMECOMPLETO, @DATANASCIMENTO, @GENERO)";
                cmd.Parameters.AddWithValue("@CPF", Cpf);
                cmd.Parameters.AddWithValue("@NOMECOMPLETO", NomeCompleto);
                cmd.Parameters.AddWithValue("@DATANASCIMENTO", DataNascimento);
                cmd.Parameters.AddWithValue("@GENERO", Genero);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
