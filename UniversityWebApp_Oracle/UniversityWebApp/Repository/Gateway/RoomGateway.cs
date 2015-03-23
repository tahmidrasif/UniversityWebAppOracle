using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class RoomGateway:Gateway
    {
         public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public RoomGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(Room aRoom)
        {
            string query = string.Format(@"INSERT INTO ROOM (ROOMNUMBER,CAPACITY,DEPARTMENTID) VALUES('" + aRoom.RoomNumber + "'," + aRoom.Capacity + "," + aRoom.DepartmentId + ")");

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
        public List<Room> GetAll()
        {
            string query = string.Format(@"SELECT * FROM ROOM");
            var rooms = new List<Room>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        Room aRoom = new Room();
                        aRoom.RoomId = Convert.ToInt16(OracleDataReader[0]);
                        aRoom.RoomNumber = OracleDataReader[1].ToString();
                        aRoom.Capacity = Convert.ToInt16(OracleDataReader[2]);
                        aRoom.DepartmentId = Convert.ToInt16(OracleDataReader[3]);
                        rooms.Add(aRoom);
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
            return rooms;

        }

        public Room GetById(int? id)
        {
            Room aRoom = new Room();
            string query = string.Format(@"SELECT * FROM ROOM WHERE ROOMID=" + id);
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
                            aRoom.RoomId = Convert.ToInt16(OracleDataReader[0]);
                            aRoom.RoomNumber = OracleDataReader[1].ToString();
                            aRoom.Capacity = Convert.ToInt16(OracleDataReader[2]);
                            aRoom.DepartmentId = Convert.ToInt16(OracleDataReader[3]);

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
            return aRoom;

        }

        public void Edit(Room aRoom)
        {

            string query = string.Format(@"UPDATE ROOM SET ROOMNUMBER='" + aRoom.RoomNumber + "',CAPACITY=" + aRoom.Capacity + ",DEPARTMENTID=" + aRoom.DepartmentId + " WHERE ROOMID=" + aRoom.RoomId);

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
            
                string query = string.Format(@"DELETE FROM  ROOM  WHERE ROOMID=" + id);
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