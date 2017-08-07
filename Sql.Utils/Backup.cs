using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Sql.Utils
{
    #region enum
    public enum SqlUtilsBackupResponse
    {
        Backup_Ok,
        Connection_Error,
        DirectoryNotFound
    }
    #endregion

    public static class _Ext
    {
        #region Generar Respaldo
        public static SqlUtilsBackupResponse Create(this Backup backup)
        {
            if (!Directory.Exists(backup.Directory))
                return SqlUtilsBackupResponse.DirectoryNotFound;

            try
            {
                SqlConnection Connection = backup.Connection;
                string SubFolder = string.Format("{0}\\{1}", backup.Directory, backup.Database);

                if (!Directory.Exists(SubFolder))
                    Directory.CreateDirectory(SubFolder);
                
                string file = string.Format("{0}\\{1}\\{2}", backup.Directory, backup.Database, backup.FileName);
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
                var command = new SqlCommand("Backup database " + backup.Database + " to disk='" + file + "'", Connection);
                try
                {
                    Connection.Open();
                    command.ExecuteNonQuery();
                    return SqlUtilsBackupResponse.Backup_Ok;
                }
                catch
                {
                    Connection.Close();
                    return SqlUtilsBackupResponse.Connection_Error;
                }
                finally
                {
                    Connection.Close();
                }
            }
            catch
            {
                return SqlUtilsBackupResponse.Connection_Error;
            }
        }
        #endregion
    }
    public class Backup
    {
        public SqlConnection Connection { get; set; }
        public string Directory { get; set; }
        public string Database { get; set; }
        public string FileName { get; set; }
    }
}
