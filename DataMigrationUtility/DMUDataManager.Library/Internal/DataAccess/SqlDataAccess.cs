using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Internal.DataAccess
{
    public class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string connectionString, string databaseName = "DMUCompanyData")
        {
            return string.Format(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString, databaseName);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName, string databaseName = "DMUCompanyData")
        {
            string connectionString = GetConnectionString(connectionStringName, databaseName);
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                List<T> rows = conn.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName, string databaseName = "DMUCompanyData")
        {
            string connectionString = GetConnectionString(connectionStringName, databaseName);
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(storedProcedure, parameters,
                     commandType: CommandType.StoredProcedure);

            }
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
                 commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartConnection(string connectionStringName, string databaseName = "DMUCompanyData")
        {
            string connectionString = GetConnectionString(connectionStringName, databaseName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {

                _transaction?.Commit();
                _connection?.Close();
            }
            catch
            {

                //throw;
            }
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        public string GetServerName(string connectingSettingName)
        {
            string serverName = "";
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectingSettingName)))
            {
                serverName = connection.DataSource;
            }
            return serverName;
        }

        public void Dispose()
        {
            CommitTransaction();
        }
    }
}
