﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class StudentGateway:Gateway
    {
         public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public StudentGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(Student student)
        {
        //    string query = string.Format(@"INSERT INTO STUDENT (REGISTRATIONNO,USERID,DEPARTMENTID) VALUES(@reg,@uId,@dId)");
            string query = string.Format(@"INSERT INTO STUDENT (REGISTRATIONNO,USERID,DEPARTMENTID) VALUES('" + student.RegistrationNo + "',"+student.UserId+","+student.DepartmentId+")");
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

        public void Edit(Student student)
        {
            var aStudent = GetById(student.StudentId);
            aStudent = student;
            string query = string.Format(@"UPDATE STUDENT SET REGISTRATIONNO='" +aStudent.RegistrationNo + "', NAME='" + aStudent.Name + "',ADDRESS='" + aStudent.Address + "',CGPA=" + aStudent.Cgpa + ",IMAGEPATH='" + aStudent.ImagePath + "',DEPARTMENTID="+aStudent.DepartmentId+"  WHERE STUDENTID=" + student.StudentId);

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
        public List<Student> GetAll()
        {
            string query = string.Format(@"SELECT * FROM STUDENT");
            var students = new List<Student>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        Student student=new Student();
                        student.StudentId= Convert.ToInt16(OracleDataReader[0]);
                        student.RegistrationNo = OracleDataReader[1].ToString();
                        student.Name = OracleDataReader[2].ToString();
                        student.Address = OracleDataReader[3].ToString();
                        if (OracleDataReader[4]!=DBNull.Value)
                        {
                            student.Cgpa = Convert.ToDouble(OracleDataReader[4]);
                        }
                        student.ImagePath = OracleDataReader[5].ToString();
                        if (OracleDataReader[6] != DBNull.Value)
                        {
                            student.DepartmentId = Convert.ToInt16(OracleDataReader[6]) ;  
                        }
                        if (OracleDataReader[7] != DBNull.Value)
                        {
                            student.UserId = Convert.ToInt16(OracleDataReader[7]);
                        }
                        students.Add(student);
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
            return students;

        }

        public Student GetById(int? id)
        {
            Student student=new Student();
            string query = string.Format(@"SELECT * FROM STUDENT WHERE STUDENTID=" + id);
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
                            student.StudentId = Convert.ToInt16(OracleDataReader[0]);
                            student.RegistrationNo = OracleDataReader[1].ToString();
                            student.Name = OracleDataReader[2].ToString();
                            student.Address = OracleDataReader[3].ToString();
                            if (OracleDataReader[4] != DBNull.Value)
                            {
                                student.Cgpa = Convert.ToDouble(OracleDataReader[4]);
                            }
                            student.ImagePath = OracleDataReader[5].ToString();
                            if (OracleDataReader[6] != DBNull.Value)
                            {
                                student.DepartmentId = Convert.ToInt16(OracleDataReader[6]);
                            }
                            if (OracleDataReader[7] != DBNull.Value)
                            {
                                student.UserId = Convert.ToInt16(OracleDataReader[7]);
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
            return student;

        }

       
        public void Delete(int? id)
        {
            
                string query = string.Format(@"DELETE FROM  STUDENT  WHERE STUDENTID=" + id);
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