using System;
using System.Data;
using System.Data.Common;

namespace Appendesk
{
    internal class DbFactory : IDisposable
    {
        private readonly DbProviderFactory _dbProviderFactory;

        private readonly ConnectionEntry _connectionEntry;

        /// <summary>
        /// 
        /// </summary>
        public DbFactory()
        {
            _connectionEntry = ConfigurationSettings.GetConnectionEntry();
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionEntry.ProviderName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        public DbFactory(string connectionName)
        {
            _connectionEntry = ConfigurationSettings.GetConnectionEntry(connectionName);
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionEntry.ProviderName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public DbCommand CreateCommand(string commandText, CommandType commandType,DbConnection connection)
        {
            try
            {
                var dbCommand = _dbProviderFactory.CreateCommand();
                if(dbCommand==null)
                    throw new ArgumentNullException(nameof(dbCommand));
                dbCommand.CommandText = commandText;
                dbCommand.CommandType = commandType;
                dbCommand.Connection = connection;
                return dbCommand;
            }
            catch (Exception)
            {
                throw new Exception("Geçersiz parametre.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection CreateConnection()
        {
            try
            {
                var connection = _dbProviderFactory.CreateConnection();
                if(connection==null)
                    throw new ArgumentNullException(nameof(connection));
                connection.ConnectionString = _connectionEntry.ConnectionString;
                return connection;
            }
            catch (Exception)
            {
                throw new Exception("Bağlantı oluşturulurken hata meydana geldi. Lütfen bağlantı dizesini ve sağlayıcı adını kontrol edin.");
            }
        }

        #region Dispose
        private bool _disposed;
        /// <summary>
        /// 
        /// </summary>
        ~DbFactory()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }
                // Note disposing has been done.
                _disposed = true;
            }
        }
        #endregion
    }
}
