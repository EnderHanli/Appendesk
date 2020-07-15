using System;
using System.Collections.Generic;
using System.Data;

namespace Appendesk
{
    public static class QueryCommandExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="prefix"></param>
        public static void AddParameter(this QueryCommand command, string parameterName, object value = null, DbType dbType = DbType.Int32, ComparisonOperator comparisonOperator = ComparisonOperator.Equals, string prefix = null)
        {
            if (command == null)
                return;
            if (value == DBNull.Value)
                value = null;
            else
                value = QueryCommandHelper.GetSqlValue(value, comparisonOperator);
            parameterName = QueryCommandHelper.GetSqlParameterName(parameterName, prefix);
            command.DynamicParameters.Add(parameterName, value, dbType, ParameterDirection.Input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="queryParameter"></param>
        public static void AddParameter(this QueryCommand command, QueryParameter queryParameter)
        {
            command.AddParameter(queryParameter.ParameterName, queryParameter.Value, queryParameter.DbType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="queryParameters"></param>
        public static void AddParameters(this QueryCommand command, List<QueryParameter> queryParameters)
        {
            foreach (var queryParameter in queryParameters)
            {
                command.AddParameter(queryParameter);
            }
        }
    }
}