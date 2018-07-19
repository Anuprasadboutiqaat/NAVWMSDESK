using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NAVWMSAPI.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace NAVWMSAPI.Repository
{
    public class WMSRepo
    {
        SqlConnection con = new SqlConnection("Data Source=192.168.8.235;Initial Catalog=NAVWMS;User ID=sa;Password=Btq@123!");
        BAL ud = new BAL();

        public DataSet GetDoclist(  int userid)
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("TEST_DOCLIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }

        public DataSet GetLogin(int stat, string u, string p, int userid)
        {
            string tkn = ud.Encrypt(DateTime.Now.ToString("dd-MON-yyyy hh:mm|"));
            con.Open();
            SqlCommand cmd = new SqlCommand("USER_EDITOR_ADDEDIT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", stat);
            cmd.Parameters.AddWithValue("@USERNAME", u);
            cmd.Parameters.AddWithValue("@USERPASSWORD", p);
            cmd.Parameters.AddWithValue("@GENERIC", tkn);
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet GETPTADOCLIST(int userid, string tokenid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PTA_DOCLIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USERID", userid);
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet GETPTALISTCONFIRM(int userid, string putno, string binno, string barcode, string itemno, string tokenid, int status)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PTA_DOCLIST_CONFIRM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", status);
            cmd.Parameters.AddWithValue("@PUTNO", putno);
            cmd.Parameters.AddWithValue("@BINNO", binno);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@ITEMNO", itemno);
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet MOVLISTCONFIRM(int userid, string docno, string binno, string barcode, string serialno, string itemno, string tokenid, string whno, int status)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MOV_DOCLIST_CONFIRM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", status);
            cmd.Parameters.AddWithValue("@DOCNO", docno);
            cmd.Parameters.AddWithValue("@BINNO", binno);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@SERIALNO", serialno);
            cmd.Parameters.AddWithValue("@ITEMNO", itemno);
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            cmd.Parameters.AddWithValue("@USERID", userid);
            cmd.Parameters.AddWithValue("@WHNO", whno);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet PIKLISTCONFIRM(int userid, string putno, string binno, string eantag, string sbcode, string barcode, string itemno,  string tokenid, int status)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("PIK_DOCLIST_CONFIRM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", status);
            cmd.Parameters.AddWithValue("@PICKNO", putno);
            cmd.Parameters.AddWithValue("@BINNO", binno);
            cmd.Parameters.AddWithValue("@TAG", eantag);
            cmd.Parameters.AddWithValue("@SBCODE", sbcode);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@ITEMNO", itemno);
            
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet CCTEST(int userid, string ccno, string binno, string zoneid, string barcode, string itemno, string tokenid, int status)
        {// TESTING PURPOSE ONLY NOT LIVE
            con.Open();
            SqlCommand cmd = new SqlCommand("CC_DOCLIST_CONFIRM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", status);
            cmd.Parameters.AddWithValue("@CCNO", ccno);
            cmd.Parameters.AddWithValue("@BINNO", binno);
            cmd.Parameters.AddWithValue("@ZONEID", zoneid);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@ITEMNO", itemno);
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
        public DataSet CCLISTCONFIRM(int userid, string ccno, string binno, string zoneid, string barcode, string itemno, string tokenid, int status)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("CC_DOCLIST_CONFIRM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STATUS", status);
            cmd.Parameters.AddWithValue("@CCNO", ccno);
            cmd.Parameters.AddWithValue("@BINNO", binno);
            cmd.Parameters.AddWithValue("@ZONEID", zoneid);
            cmd.Parameters.AddWithValue("@BARCODE", barcode);
            cmd.Parameters.AddWithValue("@ITEMNO", itemno);
            cmd.Parameters.AddWithValue("@TOKENID", tokenid);
            cmd.Parameters.AddWithValue("@USERID", userid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }
    }
}