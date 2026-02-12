using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UAS_Pemrograman_Visual
{
    class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data Source = DESKTOP-VAEGGBK; initial catalog= DB_PEMROGRAMAN_VISUAL; integrated security=true";
            return Conn;

        }
    }
}
