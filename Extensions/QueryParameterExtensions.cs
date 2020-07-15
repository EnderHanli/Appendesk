using System.Collections.Generic;
using System.Data;

namespace Appendesk
{
    public static class QueryParameterExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="logicalOperator"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static void AddQueryParameter(this List<QueryParameter> queryParameters, string parameterName, object value = null, DbType dbType = DbType.Int32, LogicalOperator logicalOperator = LogicalOperator.And, ComparisonOperator comparisonOperator = ComparisonOperator.Equals, string prefix = null)
        {
            if (queryParameters == null)
                queryParameters = new List<QueryParameter>();
            QueryParameter queryParameter = new QueryParameter(parameterName, value, dbType, logicalOperator, comparisonOperator, prefix);
            queryParameters.Add(queryParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="parameterName"></param>
        /// <param name="prefix"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static void AddOrderParameter(this List<QueryParameter> queryParameters, string parameterName, string prefix = null, OrderType orderType = OrderType.Ascending)
        {
            if (queryParameters == null)
                queryParameters = new List<QueryParameter>();

            QueryParameter queryParameter = new QueryParameter(parameterName, prefix: prefix, parameterType: ParameterType.Order, orderType: orderType);
            queryParameters.Add(queryParameter);
        }
    }
}
