using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace UniversityWebApp.Repository.Gateway
{
    public class SemesterGateway : Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public SemesterGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public List<string> GetAll()
        {
            string query = string.Format(@"SELECT * FROM SEMESTER");
            var semesters = new List<string>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        string semester = OracleDataReader[0].ToString();   
                        semesters.Add(semester);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error in inserting", exception);
            }
            finally
            {
                OracleConnection.Close();
            }
            return semesters;

        }
    }
}