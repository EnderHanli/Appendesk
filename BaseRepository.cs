﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appendesk
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly DbFactory _dbFactory;
        private DbConnection _dbConnection;
        private DbTransaction _dbTransaction;
        private readonly string _parentSavePoint;
        private string _savePoint;
        private bool _disposed;

        #region Constructor
        protected BaseRepository()
        {
            _dbFactory = new DbFactory();
            CreateConnection();
        }

        protected BaseRepository(string connectionName)
        {
            _dbFactory = new DbFactory(connectionName);
            CreateConnection();
        }

        protected BaseRepository(BaseRepository repository)
        {
            _dbFactory = repository._dbFactory;
            _dbConnection = repository._dbConnection;
            _dbTransaction = repository._dbTransaction;
            _parentSavePoint = repository._savePoint;
        }
        #endregion

        #region WhereClause
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        protected string GetWhereClause(List<QueryParameter> queryParameters)
        {
            string whereClause = "";

            List<QueryParameter> queryParametersQuery = queryParameters.FindAll(p => p.ParameterType == ParameterType.Query);
            List<QueryParameter> queryParametersOrder = queryParameters.FindAll(p => p.ParameterType == ParameterType.Order);

            if (queryParametersQuery.Count > 0)
            {
                whereClause += GetWhereClauseQuery(queryParametersQuery);
            }
            if (queryParametersOrder.Count > 0)
            {
                whereClause += GetWhereClauseOrder(queryParametersOrder);
            }
            return whereClause;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        private string GetWhereClauseQuery(List<QueryParameter> queryParameters)
        {
            string parameterMarker = ConfigurationSettings.ParameterMarker;
            var queryClause = new StringBuilder();
            foreach (QueryParameter queryParameter in queryParameters)
            {
                queryClause.Append(" ");
                queryClause.Append(queryParameter.LogicalOperator.ToString());
                queryClause.Append(" ");
                queryClause.Append(queryParameter.ParameterName);
                queryClause.Append(" ");
                switch (queryParameter.ComparisonOperator)
                {
                    case ComparisonOperator.Contains:
                        queryClause.GetContainsClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.EndsWith:
                        queryClause.GetEndsWithClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.Equals:
                        queryClause.GetEqualsClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.GreaterThan:
                        queryClause.GetGreaterThanClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.GreaterThanOrEquals:
                        queryClause.GetGreaterThanEqualsClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.In:
                        queryClause.GetInClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.LessThan:
                        queryClause.GetLessThanClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.LessThanOrEquals:
                        queryClause.GetLessThanOrEqualsClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.Like:
                        queryClause.GetLikeClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.NotEquals:
                        queryClause.GetNotEqualsClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.NotIn:
                        queryClause.GetNotInClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.NotLike:
                        queryClause.GetNotLikeClause(queryParameter, parameterMarker);
                        break;
                    case ComparisonOperator.IsNotNull:
                        queryClause.GetIsNotNullClause();
                        break;
                    case ComparisonOperator.IsNull:
                        queryClause.GetIsNullClause();
                        break;
                    case ComparisonOperator.StartsWith:
                        queryClause.GetStartsWithClause(queryParameter, parameterMarker);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return queryClause.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        private static string GetWhereClauseOrder(List<QueryParameter> queryParameters)
        {
            var orderClause = new StringBuilder();
            orderClause.Append("ORDER BY");
            orderClause.Append(" ");
            foreach (QueryParameter queryParameter in queryParameters)
            {
                switch (queryParameter.OrderType)
                {
                    case OrderType.Ascending:
                        orderClause.GetAscendingClause(queryParameter);
                        break;
                    case OrderType.Descending:
                        orderClause.GetDescendingClause(queryParameter);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return orderClause.ToString().TrimEnd(',');
        }
        #endregion

        #region Operation
        /// <summary>
        /// 
        /// </summary>
        private void CreateConnection()
        {
            _dbConnection = _dbFactory.CreateConnection();
            _dbConnection.Open();
        }

        /// <summary>
        /// 
        /// </summary>
        public void TransactionBegin()
        {
            _savePoint = Guid.NewGuid().ToString();
            if (_dbConnection == null)
            {
                CreateConnection();
            }

            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            if (_dbTransaction == null)
            {
                _dbTransaction = _dbConnection.BeginTransaction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TransactionCommit()
        {
            if (_parentSavePoint != null)
            {
                return;
            }

            _savePoint = null;
            _dbTransaction?.Commit();
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void TransactionRollback()
        {
            if (_parentSavePoint != null)
            {
                return;
            }

            _savePoint = null;
            _dbTransaction?.Rollback();
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected QueryCommand GetQueryCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            return new QueryCommand(commandText, commandType, _dbConnection, _dbTransaction);
        }
        #endregion

        #region CRUD Operation
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected IEnumerable<T> GetEntites<T>(QueryCommand command)
        {
            try
            {
                return _dbConnection.Query<T>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetEntitesAsync<T>(QueryCommand command)
        {
            try
            {
                return await _dbConnection.QueryAsync<T>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(QueryCommand command)
        {
            try
            {
                List<dynamic> results = _dbConnection.Query(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text).ToList();
                DataTable dataTable = new DataTable();
                dataTable.ToDataTable(results);
                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<DataTable> GetDataTableAsync(QueryCommand command)
        {
            try
            {
                var results = await _dbConnection.QueryAsync(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
                DataTable dataTable = new DataTable();
                dataTable.ToDataTable(results.ToList());
                return dataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected int Insert(QueryCommand command)
        {
            try
            {
                return _dbConnection.ExecuteScalar<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<int> InsertAsync(QueryCommand command)
        {
            try
            {
                return await _dbConnection.ExecuteScalarAsync<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected int Update(QueryCommand command)
        {
            try
            {
                return _dbConnection.ExecuteScalar<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected async Task<int> UpdateAsync(QueryCommand command)
        {
            try
            {
                return await _dbConnection.ExecuteScalarAsync<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected int Delete(QueryCommand command)
        {
            try
            {
                return _dbConnection.ExecuteScalar<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected async Task<int> DeleteAsync(QueryCommand command)
        {
            try
            {
                return await _dbConnection.ExecuteScalarAsync<int>(command.Sql, command.DynamicParameters, command.Transaction, commandType: command.CommandType = CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetSequence(string key)
        {
            var parameter = new DynamicParameters();

            parameter.Add("@Name", key, DbType.String, ParameterDirection.Input);
            return _dbConnection.Query<long>(ConfigurationSettings.SequenceProcName, parameter, commandType: CommandType.StoredProcedure).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="blockLength"></param>
        /// <returns></returns>
        public List<long> GetSequenceBlock(string key, long blockLength)
        {
            var sequences = new List<long>();
            for (var i = 0; i < blockLength; i++)
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Name", key, DbType.String, ParameterDirection.Input);
                var result = _dbConnection.Query<long>(ConfigurationSettings.SequenceProcName, parameter, commandType: CommandType.StoredProcedure).First();
                sequences.Add(result);
            }

            return sequences;
        }
        #endregion

        #region Dispose
        ~BaseRepository()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_dbTransaction != null)
                    {
                        _dbTransaction.Dispose();
                    }

                    if (_dbConnection != null)
                    {
                        _dbConnection.Dispose();
                    }

                    if (_dbFactory != null)
                    {
                        _dbFactory.Dispose();
                    }
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
