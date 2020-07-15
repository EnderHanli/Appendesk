using System.Data;

namespace Appendesk
{
    public class QueryParameter
    {
        public string ParameterName { get; }
        public object Value { get; }
        public DbType DbType { get; }
        public LogicalOperator LogicalOperator { get; }
        public ComparisonOperator ComparisonOperator { get; }
        public string Prefix { get; }
        public ParameterType ParameterType { get; }
        public OrderType OrderType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="logicalOperator"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="prefix"></param>
        /// <param name="parameterType"></param>
        /// <param name="orderType"></param>
        public QueryParameter(string parameterName, object value = null, DbType dbType = DbType.Int32, LogicalOperator logicalOperator = LogicalOperator.And, ComparisonOperator comparisonOperator = ComparisonOperator.Equals, string prefix = null, ParameterType parameterType = ParameterType.Query, OrderType orderType = OrderType.Ascending)
        {
            ParameterName = parameterName;
            Value = value;
            DbType = dbType;
            LogicalOperator = logicalOperator;
            ComparisonOperator = comparisonOperator;
            Prefix = prefix;
            ParameterType = parameterType;
            OrderType = orderType;
        }
    }
}
