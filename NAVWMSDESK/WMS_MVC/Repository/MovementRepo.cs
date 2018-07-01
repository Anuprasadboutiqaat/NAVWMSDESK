using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NAVWMSDESK.ViewModel.Movement;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace NAVWMSDESK.Repository
{
    public class MovementRepo
    {
        //SqlConnection con = new SqlConnection("Data Source=192.168.8.235;Initial Catalog=NAVWMS;User ID=sa;Password=Btq@123!");
        SqlConnection con = new SqlConnection(@"Database=NAVWMS;Server=192.168.11.13; user ID=ERPWEB ;Password=96V!F=I9[;Connect Timeout=2000;pooling='true'; Max Pool Size=200");
        public DataTable GetMovementList(string date1, string date2, string whno)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("WMS_DESKTOP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", 9);
            cmd.Parameters.AddWithValue("@DATE1", date1);
            cmd.Parameters.AddWithValue("@DATE2", date2);
            cmd.Parameters.AddWithValue("@WHNO", whno);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            con.Close();
            return ds;
        }


    }
}