using EmployeeApi.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace EmployeeApi.DAL
{
    public class EmployeeDAL
    {
        private string getConnString()
        {
            string connString = ""; //database connection string is written here
            return connString;
        }
        public string InsertEmployeeRecord(EmployeeModel model)
        {
            string connString = getConnString();
            String result = "";
            OracleCommand oraCmd = new OracleCommand();
            using (OracleConnection oraCon = new OracleConnection(connString))
            {
                try
                {
                    //merchant_tin_number_configuration_SEQ.NEXTVAL,
                    string SQL = "INSERT INTO EMPLOYEE (FIRST_NAME,MIDDLENAME,LASTNAME) values (:FIRST_NAME,:MIDDLE_NAME,:LAST_NAME)";
                    oraCmd.CommandText = SQL;
                    //.Parameters.Add(":ServiceUssdCode", OracleDbType.Varchar2, shortcode, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":FIRST_NAME", OracleDbType.Int32, model.FirstName, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":MIDDLE_NAME", OracleDbType.Varchar2, model.MiddleName, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":LAST_NAME", OracleDbType.Int32, model.LastName, ParameterDirection.Input);

                    oraCon.Open();
                    oraCmd.Connection = oraCon;
                    oraCmd.ExecuteNonQuery();
                    result = "Record successfully inserted.";
                    oraCon.Close();
                }
                catch (Exception ex)
                {
                    result = "Something went wrong.";
                    throw;

                }
                finally
                {
                    if (oraCon.State == ConnectionState.Open)
                    {
                        oraCon.Close();
                    }
                }
                return result;
            }

        }
        public string UpdateEmployeeRecord(EmployeeModel model)
        {
            string connString = getConnString();
            String result = "";
            OracleCommand oraCmd = new OracleCommand();
            using (OracleConnection oraCon = new OracleConnection(connString))
            {
                try
                {
                    //merchant_tin_number_configuration_SEQ.NEXTVAL,
                    string SQL = "UPDATE EMPLOYEE SET FIRST_NAME= :FIRST_NAME, MIDDLE_NAME= :MIDDLE_NAME, LAST_NAME= :LAST_NAME WHERE ID= :ID ;";
                    oraCmd.CommandText = SQL;
                    oraCmd.Parameters.Add(":ID", OracleDbType.Varchar2, model.Id, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":FIRST_NAME", OracleDbType.Int32, model.FirstName, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":MIDDLE_NAME", OracleDbType.Varchar2, model.MiddleName, ParameterDirection.Input);
                    oraCmd.Parameters.Add(":LAST_NAME", OracleDbType.Int32, model.LastName, ParameterDirection.Input);

                    oraCon.Open();
                    oraCmd.Connection = oraCon;
                    oraCmd.ExecuteNonQuery();
                    result = "Record successfully Updated.";
                    oraCon.Close();
                }
                catch (Exception ex)
                {
                    result = "Something went wrong.";
                    throw;

                }
                finally
                {
                    if (oraCon.State == ConnectionState.Open)
                    {
                        oraCon.Close();
                    }
                }
                return result;
            }

        }
        public string DeleteEmployeeRecord(int id)
        {
            string connString = getConnString();
            String result = "";
            OracleCommand oraCmd = new OracleCommand();
            using (OracleConnection oraCon = new OracleConnection(connString))
            {
                try
                {
                    //merchant_tin_number_configuration_SEQ.NEXTVAL,
                    string SQL = "DELETE FROM EMPLOYEE WHERE ID= :ID";
                    oraCmd.CommandText = SQL;
                    oraCmd.Parameters.Add(":ID", OracleDbType.Int32, id, ParameterDirection.Input);

                    oraCon.Open();
                    oraCmd.Connection = oraCon;
                    oraCmd.ExecuteNonQuery();
                    result = "Record successfully deleted.";
                    oraCon.Close();
                }
                catch (Exception ex)
                {
                    result = "Something went wrong.";
                    throw;

                }
                finally
                {
                    if (oraCon.State == ConnectionState.Open)
                    {
                        oraCon.Close();
                    }
                }
                return result;
            }

        }
        public List<EmployeeModel> GetEmployeesList()
        {
            List<EmployeeModel> lst = new List<EmployeeModel>();

            string connString = getConnString();
            OracleDataReader dr = null;
            OracleCommand Command = new OracleCommand();
            Command.CommandText = "SELECT * FROM EMPLOYEE";
            using (OracleConnection Conn = new OracleConnection(connString))
            {
                try
                {
                    Command.Connection = Conn;
                    Conn.Open();
                    dr = Command.ExecuteReader();
                    while (dr.Read())
                    {
                        lst.Add(PopulateEmployeeRecord(dr));
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (dr != null && !dr.IsClosed)
                    {
                        dr.Close();
                    }
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                    Command.Dispose();
                }
            }
        }
        public EmployeeModel GetEmployeeRecord(int id)
        {
            EmployeeModel result = new EmployeeModel();

            string connString = getConnString();
            OracleDataReader dr = null;
            OracleCommand Command = new OracleCommand();
            Command.CommandText = "SELECT * FROM EMPLOYEE WHERE ID = :ID";
            Command.Parameters.Add(":ID", OracleDbType.Int32, id, ParameterDirection.Input);

            using (OracleConnection Conn = new OracleConnection(connString))
            {
                try
                {
                    Command.Connection = Conn;
                    Conn.Open();
                    dr = Command.ExecuteReader();
                    result = PopulateEmployeeRecord(dr);
                    //while (dr.Read())
                    //{
                    //    lst.Add(PopulateEmployeeRecord(dr));
                    //}
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (dr != null && !dr.IsClosed)
                    {
                        dr.Close();
                    }
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                    }
                    Command.Dispose();
                }
            }
        }
        public EmployeeModel PopulateEmployeeRecord(IDataRecord dr)
        {
            EmployeeModel emp = new EmployeeModel
            {
                Id = Convert.ToInt32(dr["ID"]),
                FirstName = dr["FIRST_NAME"] == DBNull.Value ? default(String) : Convert.ToString(dr["FIRST_NAME"]),
                MiddleName = dr["MIDDLE_NAME"] == DBNull.Value ? default(String) : Convert.ToString(dr["MIDDLE_NAME"]),
                LastName = dr["LAST_NAME"] == DBNull.Value ? default(String) : Convert.ToString(dr["LAST_NAME"])
            };

            return emp;
        }

    }
}