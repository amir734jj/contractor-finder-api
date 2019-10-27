using System;
using Npgsql;

namespace Api.Utilities
{
    public static class ConnectionStringUtility
    {
        /// <summary>
        /// Converts connection string url to resource
        /// </summary>
        /// <param name="connectionStringUrl"></param>
        /// <returns></returns>
        public static string ConnectionStringUrlToResource(string connectionStringUrl)
        {
            var isUrl = Uri.TryCreate(connectionStringUrl, UriKind.Absolute, out var url);

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = url.Host,
                Username = url.UserInfo.Split(':')[0],
                Password = url.UserInfo.Split(':')[1],
                Database = url.LocalPath.Substring(1),
                SslMode = SslMode.Require,
                TrustServerCertificate = true,
                Pooling = true,
                ApplicationName = "milwaukee-internationals"
            };
            
            return isUrl ? connectionStringBuilder.ToString() : string.Empty;
        }
    }
}