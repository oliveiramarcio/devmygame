using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Classes
{
    public class BancoDeDados
    {
        public static SqlConnection Conexao()
        {
            string basededados = ConfigurationManager.ConnectionStrings["AWSSqlServer"].ConnectionString;
            return new SqlConnection(basededados);
        }
    }
}