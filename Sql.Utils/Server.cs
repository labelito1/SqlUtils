using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace Sql.Utils
{
    public class Server
    {
        public string PCName { get; set; }
        public string ServerName { get; set; }
        public string Instance { get; set; }
        public bool isLocal { get; set; }

        public List<Server> getList(bool localOnly)
        {
            List<Server> result = new List<Server>();
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            
            DataTable t = SmoApplication.EnumAvailableSqlServers(localOnly);

            #region Alternativa
            //DataTable table = instance.GetDataSources();
            //foreach (DataRow dr in table.Rows)
            //{
            //    string Nombre = dr.Field<string>("ServerName");
            //    string Instancia = dr.Field<string>("InstanceName");
            //    if (!string.IsNullOrEmpty(Instancia))
            //        result.Add(string.Format("{0}\\{1}", Nombre, Instancia));
            //    else
            //        result.Add(Nombre);
            //}
            #endregion

            foreach (DataRow dr in t.Rows)
            {
                Server serv = new Server();
                serv.PCName = dr.Field<string>("Name");
                serv.ServerName = dr.Field<string>("Server");
                serv.Instance = dr.Field<string>("Instance");
                serv.isLocal = dr.Field<bool>("isLocal");
                result.Add(serv);
            }
            return result;
        }
    }
}
