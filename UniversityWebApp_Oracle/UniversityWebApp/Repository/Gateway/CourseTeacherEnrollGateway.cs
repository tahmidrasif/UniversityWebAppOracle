using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class CourseTeacherEnrollGateway : Gateway
    {
         public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public CourseTeacherEnrollGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(CourseTeacherEnroll courseTeacherEnroll)
        {
            var date = courseTeacherEnroll.DateTime.ToString("yyyy/MM/dd");
            string query = string.Format(@"INSERT INTO COURSETEACHERENROLL (TEACHERID,COURSEID,SEMESTER,ENROLLDATE) VALUES(" + courseTeacherEnroll.TeacherId + "," + courseTeacherEnroll.CourseId + ",'" + courseTeacherEnroll.Semester + "',TO_DATE('" + date + "', 'YYYY-MM-DD HH:mi:ss'))");

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

        public void Edit(CourseTeacherEnroll courseTeacherEnroll)
        {

            string query = string.Format(@"UPDATE COURSETEACHERENROLL SET TEACHERID=" + courseTeacherEnroll.TeacherId + ",COURSEID=" + courseTeacherEnroll.CourseId + ",SEMESTER='" + courseTeacherEnroll.Semester + "',ENROLLDATE=TO_DATE('" + courseTeacherEnroll.DateTime + "', 'YYYY-MM-DD HH:mi:ss') WHERE COURSETEACHERENROLLID=" + courseTeacherEnroll.CourseTeacherEnrollId);

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
        public List<CourseTeacherEnroll> GetAll()
        {
            string query = string.Format(@"SELECT * FROM COURSETEACHERENROLL");
            var courseTeacherEnrollList = new List<CourseTeacherEnroll>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        var courseTeacherEnroll = new CourseTeacherEnroll();
                        courseTeacherEnroll.CourseTeacherEnrollId = Convert.ToInt16(OracleDataReader[0]);
                        courseTeacherEnroll.TeacherId = Convert.ToInt16(OracleDataReader[1]);
                        courseTeacherEnroll.CourseId = Convert.ToInt16(OracleDataReader[2]);
                        courseTeacherEnroll.Semester = OracleDataReader[3].ToString();
                        courseTeacherEnroll.DateTime = (DateTime) OracleDataReader[4];
                        courseTeacherEnrollList.Add(courseTeacherEnroll);
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
            return courseTeacherEnrollList;

        }

        public CourseTeacherEnroll GetById(int? id)
        {
            var courseTeacherEnroll = new CourseTeacherEnroll();
            string query = string.Format(@"SELECT * FROM COURSETEACHERENROLL WHERE COURSETEACHERENROLLID=" + id);
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
                            courseTeacherEnroll.CourseTeacherEnrollId = Convert.ToInt16(OracleDataReader[0]);
                            courseTeacherEnroll.TeacherId = Convert.ToInt16(OracleDataReader[1]);
                            courseTeacherEnroll.CourseId = Convert.ToInt16(OracleDataReader[2]);
                            courseTeacherEnroll.Semester = OracleDataReader[3].ToString();
                            courseTeacherEnroll.DateTime = (DateTime)OracleDataReader[4];
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
            return courseTeacherEnroll;

        }

       
        public void Delete(int? id)
        {

            string query = string.Format(@"DELETE FROM  COURSETEACHERENROLL  WHERE COURSETEACHERENROLLID=" + id);
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