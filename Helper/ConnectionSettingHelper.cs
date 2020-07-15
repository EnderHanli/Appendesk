using System;
using System.Collections.Generic;
using System.Linq;

namespace Appendesk
{
    internal class ConnectionSettingHelper
    {
        public List<ConnectionEntry> ConnectionEntries { get; set; } = new List<ConnectionEntry>();
        public ConnectionEntry GetConnectionStringEntry(string name)
        {
            ConnectionEntry returnItem = this.GetConnectionStringsEntry(name);
            return returnItem;
        }

        public ConnectionEntry GetConnectionStringsEntry(string name)
        {
            ConnectionEntry returnItem = null;
            if (null != this.ConnectionEntries && this.ConnectionEntries.Any())
            {
                returnItem = this.ConnectionEntries.FirstOrDefault(ce => ce.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            if (null == returnItem)
            {
                throw new ArgumentOutOfRangeException(string.Format("No default ConnectionStringEntry found. (ConnectionStringEntries.Names='{0}', Search.Name='{1}')", this.ConnectionEntries == null ? string.Empty : string.Join(",", this.ConnectionEntries.Select(ce => ce.Name)), name));
            }

            return returnItem;
        }
    }
}