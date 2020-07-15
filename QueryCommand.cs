using Dapper;
using System.Data;
using System.Data.Common;

namespace Appendesk
{
    public class QueryCommand
    {
        public string Sql { get; set; }

        public DynamicParameters DynamicParameters { get; set; }

        public DbTransaction Transaction { get; set; }

        public DbConnection Connection { get; set; }

        public int? CommandTimeOut { get; set; }

        public CommandType CommandType { get; set; }

        public QueryCommand()
        {
            DynamicParameters = new DynamicParameters();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public QueryCommand(string sql, CommandType commandType, DbConnection connection, DbTransaction transaction = null)
        {
            Sql = sql;
            DynamicParameters = new DynamicParameters();
            CommandType = commandType;
            Connection = connection;
            Transaction = transaction;
        }
    }
}
