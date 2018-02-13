using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace HospitalManager
{
    public class DataConsumer
    {
        /// <summary>
        /// Executes a procedure with given input values as a generic string list
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int executeProcedure(string procedureName, List<string> values)
        {
            using (OracleConnection con = new OracleConnection(HospitalClass.getConnectionString()))
            {
                using (OracleCommand cmd = new OracleCommand(procedureName, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 1; i <= values.Count; i++) cmd.Parameters.AddWithValue("param" + i, values[i - 1]).Direction = ParameterDirection.Input;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        //To show that implicit conversion occurs for object datatypes
        public static int executeProc(string procedureName, object[] values)
        {
            using (OleDbConnection con = new OleDbConnection("Provider=OraOLEDB.Oracle;" + HospitalClass.getConnectionString()))
            {
                using (OleDbCommand cmd = new OleDbCommand(procedureName, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 1; i <= values.Length; i++) cmd.Parameters.AddWithValue("param" + i, values[i - 1]).Direction = ParameterDirection.Input;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        /// <summary>
        /// Execute create, update, delete and other non-selecting queries
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int executeQuery(string query)
        {
            using (OracleConnection con = new OracleConnection(HospitalClass.getConnectionString()))
            {
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.Text;  //default
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Sends a picture encoded as a byte array to the database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int sendPictureToDatabase(string query, byte[] a)
        {
            using (OracleConnection con = new OracleConnection(HospitalClass.getConnectionString()))
            {
                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("image", a);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

    }
}