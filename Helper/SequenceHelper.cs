using System.Configuration;
using System.Text;

namespace Appendesk
{
    public static class SequenceHelper
    {
        private static readonly string ProjectPrefix = "Tst";//ConfigurationManager.AppSettings["ProjectPrefix"];

        public static string GetSequenceName(this BusinessLayer businessLayer, string key)
        {
            var stringBuilder=new StringBuilder();
            stringBuilder.Append(ProjectPrefix)
                .Append("_")
                .Append(businessLayer.GetType().Name)
                .Append("_")
                .Append(key);
            return stringBuilder.ToString();
        }
    }
}