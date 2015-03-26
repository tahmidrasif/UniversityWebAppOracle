using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Areas.Teacher.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class CourseStudentEnrollGateway:Gateway
    {
         public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public CourseStudentEnrollGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(CourseStudentEnroll courseStudentEnroll)
        {
            var date = courseStudentEnroll.DateTime.ToString("yyyy/MM/dd");
            string query = string.Format(@"INSERT INTO COURSESTUDENTENROLL (STUDENTID ,COURSEID,SEMESTER,ENROLLDATE) VALUES(" + courseStudentEnroll.StudentId + "," + courseStudentEnroll.CourseId + ",'" + courseStudentEnroll.Semester + "',TO_DATE('" + date + "', 'YYYY-MM-DD HH:mi:ss'))");

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

        public void Edit(CourseStudentEnroll courseStudentEnroll)
        {
            var date = courseStudentEnroll.DateTime.ToString("yyyy/MM/dd");
            string query = string.Format(@"UPDATE COURSESTUDENTENROLL SET STUDENTID=" + courseStudentEnroll.StudentId + ",COURSEID=" + courseStudentEnroll.CourseId + ",SEMESTER='" + courseStudentEnroll.Semester + "',ENROLLDATE=TO_DATE('" + date + "', 'YYYY-MM-DD HH:mi:ss') WHERE COURSETEACHERENROLLID=" + courseStudentEnroll.CourseStudentEnrollId);

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

        public void EditByTeacher(TeacherCourseResultViewModel teachercourseresultviewmodel)
        {

            string query = string.Format(@"UPDATE COURSESTUDENTENROLL SET SCORE=" + teachercourseresultviewmodel.Score + " WHERE STUDENTID=" + teachercourseresultviewmodel.StudentId + " AND COURSEID=" + teachercourseresultviewmodel.CourseId);

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

        public List<CourseStudentEnroll> GetAll()
        {
            string query = string.Format(@"SELECT * FROM COURSESTUDENTENROLL");
            var courseStudentEnrollList = new List<CourseStudentEnroll>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        var courseStudentEnroll = new CourseStudentEnroll();
                        courseStudentEnroll.CourseStudentEnrollId = Convert.ToInt16(OracleDataReader[0]);
                        courseStudentEnroll.StudentId = Convert.ToInt16(OracleDataReader[1]);
                        courseStudentEnroll.CourseId = Convert.ToInt16(OracleDataReader[2]);
                        courseStudentEnroll.Semester = OracleDataReader[3].ToString();
                        courseStudentEnroll.DateTime = (DateTime)OracleDataReader[4];
                        if (OracleDataReader[5] != DBNull.Value)
                        {
                            courseStudentEnroll.Score = Convert.ToDouble(OracleDataReader[5]);
                        }
                        courseStudentEnrollList.Add(courseStudentEnroll);
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
            return courseStudentEnrollList;

        }

        public CourseStudentEnroll GetById(int? id)
        {
            var courseStudentEnroll = new CourseStudentEnroll();
            string query = string.Format(@"SELECT * FROM COURSESTUDENTENROLL WHERE COURSESTUDENTENROLLID=" + id);
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
                            courseStudentEnroll.CourseStudentEnrollId = Convert.ToInt16(OracleDataReader[0]);
                            courseStudentEnroll.StudentId = Convert.ToInt16(OracleDataReader[1]);
                            courseStudentEnroll.CourseId = Convert.ToInt16(OracleDataReader[2]);
                            courseStudentEnroll.Semester = OracleDataReader[3].ToString();
                            courseStudentEnroll.DateTime = (DateTime)OracleDataReader[4];
                            if (OracleDataReader[5] != DBNull.Value)
                            {
                                courseStudentEnroll.Score = Convert.ToDouble(OracleDataReader[5]);
                            }
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
            return courseStudentEnroll;

        }


        public void Delete(int? id)
        {

            string query = string.Format(@"DELETE FROM  COURSESTUDENTENROLL  WHERE COURSESTUDENTENROLLID=" + id);
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