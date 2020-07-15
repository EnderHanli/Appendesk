using System;
using System.Collections.Generic;
using System.Data;

namespace Appendesk
{
    internal static class QueryCommandHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonOperator"></param>
        /// <returns></returns>
        public static string GetSqlValue(object value, ComparisonOperator comparisonOperator)
        {
            if (value == null)
                return string.Empty;

            if (comparisonOperator == ComparisonOperator.Contains)
            {
                value = "%" + value + "%";
            }
            else if (comparisonOperator == ComparisonOperator.EndsWith)
            {
                value = "%" + value;
            }
            else if (comparisonOperator == ComparisonOperator.StartsWith)
            {
                value += "%";
            }

            return value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetSqlParameterName(string parameterName, string prefix)
        {
            return string.IsNullOrEmpty(prefix) ? parameterName : prefix + "." + parameterName;
        }
    }
}
