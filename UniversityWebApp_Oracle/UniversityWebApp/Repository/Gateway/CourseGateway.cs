using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class CourseGateway:Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public CourseGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(Course course)
        {
            string query = string.Format(@"INSERT INTO COURSE (NAME,CODE,CREDIT,DEPARTMENTID) VALUES('" + course.Name + "','" + course.Code + "'," + course.Credit + "," + course.DepartmentId + ")");

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

        public void Edit(Course course)
        {

            string query = string.Format(@"UPDATE COURSE SET NAME='" + course.Name + "',CODE='" + course.Code + "',CREDIT=" + course.Credit + ",DEPARTMENTID=" + course.DepartmentId+ " WHERE COURSEID=" + course.CourseId);

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
        public List<Course> GetAll()
        {
            string query = string.Format(@"SELECT * FROM COURSE");
            var courses = new List<Course>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        Course course=new Course();
                        course.CourseId= Convert.ToInt16(OracleDataReader[0]);
                        course.Name = (string)OracleDataReader[1];
                        course.Code = (string)OracleDataReader[2];
                        course.Credit = Convert.ToDouble(OracleDataReader[3]);
                        course.DepartmentId = Convert.ToInt16(OracleDataReader[4]);
                        courses.Add(course);
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
            return courses;

        }

        public Course GetById(int? id)
        {
            Course course=new Course();
            string query = string.Format(@"SELECT * FROM COURSE WHERE COURSEID=" + id);
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
                            course.CourseId = Convert.ToInt16(OracleDataReader[0]);
                            course.Name = (string)OracleDataReader[1];
                            course.Code = (string)OracleDataReader[2];
                            course.Credit = Convert.ToDouble(OracleDataReader[3]);
                            course.DepartmentId = Convert.ToInt16(OracleDataReader[4]);
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
            return course;

        }

       
        public void Delete(int? id)
        {
            
                string query = string.Format(@"DELETE FROM  COURSE  WHERE COURSEID=" + id);
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