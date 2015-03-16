using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace UniversityWebApp.Repository.Gateway
{
    public class Gateway
    {
        public OracleConnection OracleConnection{ get; set; }

        public Gateway(string connectionName)
        {
            OracleConnection = new OracleConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);

        }

    }
}