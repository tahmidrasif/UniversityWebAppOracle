using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class DepartmentGateway:Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public DepartmentGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(Department aDepartment)
        {
            string query = string.Format(@"INSERT INTO DEPARTMENT (NAME,CODE) VALUES('" + aDepartment.Name + "','" + aDepartment.Code + "')");

            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                int isAffected = OracleCommand.ExecuteNonQuery();
                return isAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Error in inserting", exception);
            }
            finally
            {
                OracleConnection.Close();
            }

        }
        public List<Department> GetAll()
        {
            string query = string.Format(@"SELECT * FROM DEPARTMENT");
            var depatments = new List<Department>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        Department aDepartment = new Department();
                        aDepartment.DepartmentId = Convert.ToInt16(OracleDataReader[0]);
                        aDepartment.Name = (string)OracleDataReader[1];
                        aDepartment.Code = (string)OracleDataReader[2];
                        depatments.Add(aDepartment);
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
            return depatments;

        }

        public Department GetById(int? id)
        {
            Department aDepartment = new Department();
            string query = string.Format(@"SELECT * FROM DEPARTMENT WHERE DEPARTMENTID=" + id);
            try
            {
                if (id != null)
                {

                    OracleConnection.Open();
                    OracleCommand = new OracleCommand(query, OracleConnection);
                    OracleDataReader = OracleCommand.ExecuteReader();
                    if (OracleDataReader.HasRows)
                    {
                        while (OracleDataReader.Read())
                        {
                            aDepartment.DepartmentId = Convert.ToInt16(OracleDataReader[0]);
                            aDepartment.Name = (string)OracleDataReader[1];
                            aDepartment.Code = (string)OracleDataReader[2];

                        }
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
            return aDepartment;

        }

        public void Edit(Department aDepartment)
        {

            string query = string.Format(@"UPDATE DEPARTMENT SET NAME='" + aDepartment.Name + "',CODE='" + aDepartment.Code + "' WHERE DEPARTMENTID=" + aDepartment.DepartmentId);

            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                int isAffected = OracleCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Error in inserting", exception);
            }
            finally
            {
                OracleConnection.Close();
            }

        }
        public void Delete(int? id)
        {
            
                string query = string.Format(@"DELETE FROM  DEPARTMENT  WHERE DEPARTMENTID=" + id);
                try
                {
                    if (id != null)
                    {
                        OracleConnection.Open();
                        OracleCommand = new OracleCommand(query, OracleConnection);
                        int isAffected = OracleCommand.ExecuteNonQuery();
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
           
        }

    }
}