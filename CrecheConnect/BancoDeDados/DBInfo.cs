using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheConnect.BancoDeDados
{
    internal class DBInfo
    {
        internal const string DBConnection = @"Data Source=localhost;Initial Catalog=CRECHECONNECT;User ID=sa;Password=sa";

        internal static bool TestDBConnection()
        {
            var result = false;

            using (var conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT (*) FROM ALUNOS";

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"O teste de contagem foi executado e retornou {reader.GetInt32(0)} registros em Alunos.");
                }

                //conn.Close();
            }

            return result;
        }
    }
}
