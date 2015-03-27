using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class CourseRoomEnrollGateway:Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public CourseRoomEnrollGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(CourseRoomEnroll courseRoomEnroll)
        {
            var date = courseRoomEnroll.Date.ToString("yyyy/MM/dd");
            var startTime= courseRoomEnroll.StratingTime.ToString("hh:mm:ss");
            var endTime = courseRoomEnroll.EndTime.ToString("hh:mm:ss");
            string query = string.Format(@"INSERT INTO COURSEROOMENROLL (COURSEID ,ROOMID, TEACHERID,ENROLLDATE,STARTTIME,ENDTIME) VALUES(" + courseRoomEnroll.CourseId + "," + courseRoomEnroll.RoomId + "," + courseRoomEnroll.TeacherId + ",TO_DATE('" + date + "', 'YYYY-MM-DD'),TO_DATE('" + startTime + "', 'HH:mi:ss'),TO_DATE('" + endTime + "', 'HH:mi:ss'))");

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

        //public void EditByTeacher(TeacherCourseResultViewModel teachercourseresultviewmodel)
        //{

        //    string query = string.Format(@"UPDATE COURSESTUDENTENROLL SET SCORE=" + teachercourseresultviewmodel.Score + " WHERE STUDENTID=" + teachercourseresultviewmodel.StudentId + " AND COURSEID=" + teachercourseresultviewmodel.CourseId);

        //    try
        //    {
        //        OracleConnection.Open();
        //        OracleCommand = new OracleCommand(query, OracleConnection);
        //        int isAffected = OracleCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception("Error in inserting", exception);
        //    }
        //    finally
        //    {
        //        OracleConnection.Close();
        //    }

        //}

        public List<CourseRoomEnroll> GetAll()
        {
            string query = string.Format(@"SELECT * FROM COURSEROOMENROLL");
            var courseRoomEnrollList = new List<CourseRoomEnroll>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        var courseRoomEnroll = new CourseRoomEnroll();
                        courseRoomEnroll.CourseRoomEnrollId = Convert.ToInt16(OracleDataReader[0]);
                        courseRoomEnroll.CourseId = Convert.ToInt16(OracleDataReader[1]);
                        courseRoomEnroll.RoomId = Convert.ToInt16(OracleDataReader[2]);
                        courseRoomEnroll.TeacherId = Convert.ToInt16(OracleDataReader[3]);
                        courseRoomEnroll.Date = (DateTime)OracleDataReader[4];
                        courseRoomEnroll.StratingTime = (DateTime)OracleDataReader[5];
                        courseRoomEnroll.EndTime = (DateTime)OracleDataReader[6];

                        courseRoomEnrollList.Add(courseRoomEnroll);
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
            return courseRoomEnrollList;

        }

        public CourseStudentEnroll GetById(int? id)
        {
            var courseStudentEnroll = new CourseStudentEnroll();
            string query = string.Format(@"SELECT * FROM COURSEROOMENROLL WHERE COURSESTUDENTENROLLID=" + id);
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

        public List<CourseRoomEnroll> GetByRoomId(int roomId, DateTime date)
        {
            var adate = date.ToString("yyyy/MM/dd");
            string query = string.Format(@"SELECT * FROM COURSEROOMENROLL WHERE ROOMID=" + roomId + " AND ENROLLDATE=TO_DATE('" + adate + "', 'YYYY-MM-DD')");
            var courseRoomEnrollList = new List<CourseRoomEnroll>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        var courseRoomEnroll = new CourseRoomEnroll();
                        courseRoomEnroll.CourseRoomEnrollId = Convert.ToInt16(OracleDataReader[0]);
                        courseRoomEnroll.CourseId = Convert.ToInt16(OracleDataReader[1]);
                        courseRoomEnroll.RoomId = Convert.ToInt16(OracleDataReader[2]);
                        courseRoomEnroll.TeacherId = Convert.ToInt16(OracleDataReader[3]);
                        courseRoomEnroll.Date = (DateTime)OracleDataReader[4];
                        courseRoomEnroll.StratingTime = (DateTime)OracleDataReader[5];
                        courseRoomEnroll.EndTime = (DateTime)OracleDataReader[6];

                        courseRoomEnrollList.Add(courseRoomEnroll);
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
            return courseRoomEnrollList;
        }
    }
}