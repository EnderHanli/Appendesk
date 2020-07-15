using System.Text;

namespace Appendesk
{
    internal static class StringBuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetContainsClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("LIKE")
                .Append(" ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetEndsWithClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("LIKE")
                .Append(" ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetEqualsClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" = ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetGreaterThanClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" > ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetGreaterThanEqualsClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" >= ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetInClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" IN( ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName)
                .Append(")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetLessThanClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" < ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetLessThanOrEqualsClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append(" <= ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetLikeClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("LIKE")
                .Append(" ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetNotEqualsClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("!=")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetNotInClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("NOT IN(")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName)
                .Append(")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetNotLikeClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("NOT LIKE")
                .Append(" ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stringBuilder"></param>
        public static void GetIsNotNullClause(this StringBuilder stringBuilder)
        {
            (stringBuilder ?? new StringBuilder()).Append("IS NOT NULL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        public static void GetIsNullClause(this StringBuilder stringBuilder)
        {
            (stringBuilder ?? new StringBuilder()).Append("IS NULL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        /// <param name="parameterMarker"></param>
        public static void GetStartsWithClause(this StringBuilder stringBuilder, QueryParameter queryParameter, string parameterMarker)
        {
            (stringBuilder ?? new StringBuilder()).Append("LIKE")
                .Append(" ")
                .Append(parameterMarker)
                .Append(queryParameter.ParameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        public static void GetAscendingClause(this StringBuilder stringBuilder, QueryParameter queryParameter)
        {
            (stringBuilder ?? new StringBuilder()).Append(" ")
                .Append(queryParameter.ParameterName)
                .Append(" ")
                .Append("Asc")
                .Append(",");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="queryParameter"></param>
        public static void GetDescendingClause(this StringBuilder stringBuilder, QueryParameter queryParameter)
        {
            (stringBuilder ?? new StringBuilder()).Append(" ")
                .Append(queryParameter.ParameterName)
                .Append(" ")
                .Append("Desc")
                .Append(",");
        }
    }
}
