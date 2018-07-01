using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NAVWMSWEB.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace NAVWMSWEB.Repository
{
   
    public class NAVWMSREPO
    {
        // SqlConnection con = new SqlConnection("Data Source=192.168.8.235;Initial Catalog=NAVWMS;User ID=sa;Password=Btq@123!");
        SqlConnection con = new SqlConnection(@"Database=NAVWMS;Server=192.168.11.13; user ID=ERPWEB ;Password=96V!F=I9[;Connect Timeout=2000;pooling='true'; Max Pool Size=200");
        BAL ud = new BAL();
        
        public DataTable GETPTADOCLIST(int userid )
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("TEST_DOCLIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;

        }
    }
}