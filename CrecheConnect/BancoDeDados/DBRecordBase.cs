using CrecheConnect.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CrecheConnect.BancoDeDados
{
    internal abstract class DBRecordBase : IDBRecord
    {
        public abstract string TableName { get; }
        public abstract int Id { get; }
        public abstract string Cpf { get; set; }
        public abstract string NomeCompleto { get; set; }
        public abstract DateTime DataNascimento { get; set; }
        public abstract Enumeradores.Genero Genero { get; set; }

        //public void Save()
        //{

        //}

        public void Delete()
        {
            using (var conn = new SqlConnection(DBInfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"DELETE FROM {TableName.ToUpper()} WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update()
        {
            using (var conn = new SqlConnection(DBInfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                //cmd.CommandText = "UPDATE ALUNOS SET CPF = @CPF, NOMECOMPLETO = @NOMECOMPLETO, DATANASCIMENTO = @DATANASCIMENTO, GENERO = @GENERO WHERE ID = @ID";
                cmd.CommandText = $"UPDATE {TableName.ToUpper()} SET CPF = @CPF, NOMECOMPLETO = @NOMECOMPLETO, DATANASCIMENTO = @DATANASCIMENTO, GENERO = @GENERO WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@CPF", Cpf);
                cmd.Parameters.AddWithValue("@NOMECOMPLETO", NomeCompleto);
                cmd.Parameters.AddWithValue("@DATANASCIMENTO", DataNascimento);
                cmd.Parameters.AddWithValue("@GENERO", Genero);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Aluno> Read()
        {

            var result = new List<Aluno>();

            using (var conn = new SqlConnection(DBInfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT ID, CPF, NOMECOMPLETO, DATANASCIMENTO, GENERO FROM ALUNOS";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), (Enumeradores.Genero)reader.GetInt32(4));
                        result.Add(aluno);
                    }
                }
            }

            return result;

        }
    }
}
