
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
    public class DBTEST
    {
        SqlConnection con;
        SqlCommand cmd;
        /* *****  Function for sql connection details   *** */
        public DBTEST()
        {
            con = new SqlConnection(@"Database=NAVWMS;Server=192.168.11.13; user ID=ERPWEB;Password=96V!F=I9[;Connect Timeout=200;pooling='true'; Max Pool Size=200");

            cmd = new SqlCommand();
        }

        /* *****  Function for sql connection open   *** */
        public SqlConnection GETCONNECTION()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }
            return con;
        }
        /* *****  Execute reader function for string  *** */
        public SqlDataReader EXCUTEREADER_FUNCT(String str)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandText = str;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;

        }
        /* *****  Execute non query function for string  *** */
        public void EXECUTENONQUERY_FUNCT(String str)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandText = str;
            cmd.ExecuteNonQuery();
        }
        /* *****  Execute data table function for string  *** */
        public DataTable EXECUTEDATATABLE_FUNCT(String str)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandText = str;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataSet EXECUTEDATASET_FUNCT(String str)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandText = str;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }
        /* *****  Execute scalar function for string  *** */
        public object EXECUTESCALAR_FUNCTION(String str)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandText = str;
            object ob = cmd.ExecuteScalar();
            return ob;
        }
        /* *****  Close sql connection  *** */
        public void dbclose()
        {
            con.Close();
        }


        /* *****  Execute non query function for procedure  *** */
        public void EXECUTENONQUERY_PROCE_FUNCT(SqlCommand cmd)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
        }

    public int  EXECUTENONQUERY_PROCE_FUNCTION(SqlCommand cmd)
    {
        cmd.Connection = GETCONNECTION();
        cmd.CommandType = CommandType.StoredProcedure;
       int I= cmd.ExecuteNonQuery();
        con.Close();
        return I;
    }


    /* *****  Execute reader function for procedure  *** */
    public SqlDataReader EXCUTEREADER_PROCE_FUNCT(SqlCommand cmd)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;


        }
        /* *****  Execute datatable function for procedure  *** */
        public DataTable EXECUTEDATATABLE_PROCE_FUNCT(SqlCommand cmd)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            return dt;
        }
        public DataSet EXECUTEDATASET_PROCE_FUNCT(SqlCommand cmd)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            Task.WaitAll(Task.Delay(2000));
            da.Dispose();
            cmd.CommandTimeout = 300;
            //  con.ConnectionTimeout = "1200";
            con.Close();
            return dt;
        }

        /* *****  Execute scalar function for procedure  *** */
        public object EXECUTESCALAR_PROCE_FUNCT(SqlCommand cmd)
        {
            cmd.Connection = GETCONNECTION();
            cmd.CommandType = CommandType.StoredProcedure;
            object ob = cmd.ExecuteScalar();
            return ob;
        }
    }
