using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class UserGateway : Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public UserGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(User aUser)
        {
            string query = string.Format(@"INSERT INTO USERS (USERNAME,PASSWORD,EMAIL,USERTYPE) VALUES('" + aUser.UserName + "','" + aUser.Password + "','" + @aUser.Email + "','" + aUser.UserType + "')");

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
        public List<User> GetAll()
        {
            string query = string.Format(@"SELECT * FROM USERS");
            var users = new List<User>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        User aUser = new User();
                        aUser.UserId = Convert.ToInt16(OracleDataReader[0]);
                        aUser.UserName = (string)OracleDataReader[1];
                        aUser.Password = (string)OracleDataReader[2];
                        aUser.Email = (string)OracleDataReader[3];
                        aUser.UserType = (string)OracleDataReader[4];
                        users.Add(aUser);
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
            return users;

        }

        public User GetById(int? id)
        {
            User aUser = new User();
            string query = string.Format(@"SELECT * FROM USERS WHERE USERID=" + id);
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
                            aUser.UserId = Convert.ToInt16(OracleDataReader[0]);
                            aUser.UserName = (string)OracleDataReader[1];
                            aUser.Password = (string)OracleDataReader[2];
                            aUser.Email = (string)OracleDataReader[3];
                            aUser.UserType = (string)OracleDataReader[4];

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
            return aUser;

        }

        public void Edit(User aUser)
        {

            string query = string.Format(@"UPDATE USERS SET USERNAME='" + aUser.UserName + "',PASSWORD='" + aUser.Password + "',EMAIL='" + aUser.Email + "',USERTYPE='" + aUser.UserType + "' WHERE USERID=" + aUser.UserId);

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
            
                string query = string.Format(@"DELETE FROM  USERS  WHERE USERID=" + id);
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