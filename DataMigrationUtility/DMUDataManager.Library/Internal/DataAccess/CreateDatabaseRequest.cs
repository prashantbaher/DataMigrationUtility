using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Internal.DataAccess
{
    public class CreateDatabaseRequest
    {
        public string GetConnectionString(string connectionSettingName)
        {
            return ConfigurationManager.ConnectionStrings[connectionSettingName].ConnectionString;
        }

        public string ConnectToDatabse(string connectionSettingName, string databaseName)
        {
            string connectionString = string.Format(GetConnectionString(connectionSettingName), "Master");
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.ChangeDatabase(databaseName);
                return connection.ConnectionString;
            }
        }

        public string CreateDatabse(string connectionSettingName, string databaseName)
        {
            string connectionString = string.Format(GetConnectionString(connectionSettingName), "Master");
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //connection.ChangeDatabase("Master");
                connection.Open();

                string queryFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DatabaseScript");
                string createDatabaseQuery = $"USE [master] \r\n\r\nCREATE DATABASE [{databaseName}]";
                connection.Execute(createDatabaseQuery, commandType: CommandType.Text);
                connection.ChangeDatabase(databaseName);
                foreach (var filePath in Directory.GetFiles(queryFolder, "*.Table.sql").OrderBy(_ => _))
                {
                    using (var stream = new StreamReader(filePath))
                    {
                        string createQuery = string.Format(stream.ReadToEnd(), databaseName);
                        connection.Execute(createQuery, commandType: CommandType.Text);
                    }
                }
                connection.ChangeDatabase(databaseName);
                foreach (var filePath in Directory.GetFiles(queryFolder, "*.StoredProcedure.sql"))
                {
                    using (var stream = new StreamReader(filePath))
                    {
                        string createQuery = string.Format(stream.ReadToEnd(), databaseName);
                        connection.Execute(createQuery, commandType: CommandType.Text);
                    }
                }
                return connection.ConnectionString;
            }
        }


    }
}
