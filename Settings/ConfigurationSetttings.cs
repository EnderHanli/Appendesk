using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Appendesk
{
    internal static class ConfigurationSettings
    {
        private const string AppSettingFilename = "appsettings.json";
        private const string DefaultConnectionName = "DefaultConnection";
        public const string ParameterMarker = "@";
        public const string SequenceProcName = "GetSequence";
        public static ConnectionEntry GetConnectionEntry()
        {
            return GetConnectionEntryAppSetting(DefaultConnectionName);
        }

        public static ConnectionEntry GetConnectionEntry(string connectionName)
        {
            return GetConnectionEntryAppSetting(connectionName);
        }

        private static ConnectionEntry GetConnectionEntryAppSetting(string name)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingFilename, optional: true, reloadOnChange: true)
                .Build();

            ConnectionSettingHelper settings = new ConnectionSettingHelper();
            config.Bind("ConnectionStrings", settings);
            Console.WriteLine(settings.ConnectionEntries.Count);
            return settings.GetConnectionStringEntry(name);
        }
    }
}