using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sql.Utils
{
    public class Database
    {
        public List<string> getList(SqlConnection Connection)
        {
            List<string> list = new List<string>();
            DataTable table = null;
            try
            {
                Connection.Open();
                table = Connection.GetSchema("Databases");
            }
            catch 
            {
                Connection.Close();
                return list;
            }
            finally
            {
                Connection.Close();
            }
            if(table == null || table.Rows.Count == 0)
                return list;
            
            foreach (DataRow bd in table.Rows)
            {
                string nombre = bd.Field<string>("database_name");
                list.Add(nombre);
            }
            return list;
        }
    }
}
