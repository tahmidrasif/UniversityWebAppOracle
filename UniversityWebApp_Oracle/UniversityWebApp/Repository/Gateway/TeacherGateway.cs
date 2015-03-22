using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Areas.Teacher.Models;

namespace UniversityWebApp.Repository.Gateway
{
    public class TeacherGateway:Gateway
    {
        public OracleCommand OracleCommand { get; set; }
        public OracleDataReader OracleDataReader { get; set; }
        public TeacherGateway()
            : base("UniversityWebAppOracle")
        {

        }

        public int Insert(Teacher teacher)
        {
            string query = string.Format(@"INSERT INTO TEACHER (USERID,NAME,DESIGNATION,EMAIL,CREDTITOBETAKEN,REMAININGCREDIT,DEPARTMENTID,IMAGEPATH) VALUES(" + teacher.UserId + ",'" + teacher.Name + "','" + teacher.Designation + "','" + teacher.Email + "'," + teacher.CreditToBeTaken + "," + teacher.RemainingCredit + "," + teacher.DepartmentId + ",'" + teacher.ImagePath + "')");

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
        public List<Teacher> GetAll()
        {
            string query = string.Format(@"SELECT * FROM TEACHER");
            var teachers = new List<Teacher>();
            try
            {
                OracleConnection.Open();
                OracleCommand = new OracleCommand(query, OracleConnection);
                OracleDataReader = OracleCommand.ExecuteReader();
                if (OracleDataReader.HasRows)
                {
                    while (OracleDataReader.Read())
                    {
                        Teacher aTeacher=new Teacher();
                        aTeacher.TeacherId = Convert.ToInt16(OracleDataReader[0]);
                        aTeacher.UserId = Convert.ToInt16(OracleDataReader[1]);
                        aTeacher.Name = OracleDataReader[2].ToString();
                        aTeacher.Designation = OracleDataReader[3].ToString();
                        aTeacher.Email = OracleDataReader[4].ToString();
                        aTeacher.CreditToBeTaken = Convert.ToDouble(OracleDataReader[5]);
                        aTeacher.RemainingCredit = Convert.ToDouble(OracleDataReader[6]);
                        aTeacher.DepartmentId = Convert.ToInt16(OracleDataReader[7]);
                        aTeacher.ImagePath = OracleDataReader[8].ToString();
                        teachers.Add(aTeacher);
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
            return teachers;

        }

        public Teacher GetById(int? id)
        {
            Teacher aTeacher = new Teacher();
            string query = string.Format(@"SELECT * FROM TEACHER WHERE TEACHERID=" + id);
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
                            aTeacher.TeacherId = Convert.ToInt16(OracleDataReader[0]);
                            aTeacher.UserId = Convert.ToInt16(OracleDataReader[1]);
                            aTeacher.Name = OracleDataReader[2].ToString();
                            aTeacher.Designation = OracleDataReader[3].ToString();
                            aTeacher.Email = OracleDataReader[4].ToString();
                            aTeacher.CreditToBeTaken = Convert.ToDouble(OracleDataReader[5]);
                            aTeacher.RemainingCredit = Convert.ToDouble(OracleDataReader[6]);
                            aTeacher.DepartmentId = Convert.ToInt16(OracleDataReader[7]);
                            aTeacher.ImagePath = OracleDataReader[8].ToString();

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
            return aTeacher;

        }

        //EDIT BY ADMIN
        public void Edit(Teacher aTeacher)
        {

            string query = string.Format(@"UPDATE TEACHER SET USERID=" + aTeacher.UserId + ",NAME='" + aTeacher.Name + "',DESIGNATION='" + aTeacher.Designation + "',EMAIL='" + aTeacher.Email + "',CREDTITOBETAKEN=" + aTeacher.CreditToBeTaken + ",REMAININGCREDIT=" + aTeacher.RemainingCredit + ",DEPARTMENTID=" + aTeacher.DepartmentId + ",IMAGEPATH='" + aTeacher.ImagePath + "' WHERE TEACHERID=" + aTeacher.TeacherId);

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

            string query = string.Format(@"DELETE FROM  TEACHER  WHERE TEACHERID=" + id);
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

        public void EditByTeacher(TeacherViewModel aViewModel)
        {
            string query = string.Format(@"UPDATE TEACHER SET NAME='" + aViewModel.Name + "',EMAIL='" + aViewModel.Email + "',IMAGEPATH='" + aViewModel.ImagePath + "' WHERE TEACHERID=" + aViewModel.TeacherId);

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
    }
}