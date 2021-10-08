using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkpaideutikoLogismikoREAL
{
    class RepTest
    {
        //Getter Setter Εντολές
        public int RepTestID { get; set; }
        public int RightSum { get; set; }
        public int WrongSum { get; set; }
        public int Prep1 { get; set; }
        public int Prep2 { get; set; }
        public int Prep3 { get; set; }
        public int Prep4 { get; set; }
        public int Prep5 { get; set; }
        public int Prep6 { get; set; }
        public int Prep7 { get; set; }
        public int Prep8 { get; set; }
        public int Prep9 { get; set; }
        public int UserID { get; set; }

        static string myconnstring = "Data Source=DESKTOP-QPGEM5G;Initial Catalog=EkpaideutikoLogismikoDB;Integrated Security=True";

        //Select απο βάση δεδομένων
        public DataTable Select()
        {
            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "Select * from RepTest";
                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Adapter χρησιμοποιώντας το cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //Γεμίζουμε τον adapter με το table που κρατάει τα δεδομένα απο την βάση
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;

        }

        //Select count απο βάση δεδομένων
        public DataTable SelectCount(RepTest c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT COUNT(*) "+
                                "FROM RepTest "+
                                "INNER JOIN UserInformation ON RepTest.UserID = UserInformation.UserID " +
                                "WHERE UserInformation.UserID = @UserID";

                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserID", c.UserID);

                //Adapter χρησιμοποιώντας το cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //Γεμίζουμε τον adapter με το table που κρατάει τα δεδομένα απο την βάση
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Select count απο βάση δεδομένων
        public DataTable SelectLatest(RepTest c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT TOP 1 * "+
                                "FROM RepTest "+
                                "WHERE UserID = @UserID " +
                                "ORDER BY RepTestID DESC";

                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserID", c.UserID);

                //Adapter χρησιμοποιώντας το cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //Γεμίζουμε τον adapter με το table που κρατάει τα δεδομένα απο την βάση
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Select based on user απο βάση δεδομένων
        public DataTable SelectFromUser(RepTest c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT * " +
                                "FROM RepTest " +
                                "WHERE UserID = @UserID";

                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserID", c.UserID);

                //Adapter χρησιμοποιώντας το cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //Γεμίζουμε τον adapter με το table που κρατάει τα δεδομένα απο την βάση
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        //Συνάρτηση για το insert της βάσης 
        public bool Insert(RepTest c)
        {
            //Η τιμή που θα γίνει return
            bool isSuccess = false;
            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Query βάσης για το insert
                string sql = "INSERT INTO RepTest (RepTestID,RightSum,WrongSum,Prep1,Prep2" +
                    ",Prep3,Prep4,Prep5,Prep6,Prep7,Prep8,Prep9,UserID) VALUES (@RepTestID,@RightSum,@WrongSum,@Prep1,@Prep2" +
                    ",@Prep3,@Prep4,@Prep5,@Prep6,@Prep7,@Prep8,@Prep9,@UserID)";
                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Δημιουργία παραμέτρων για να γίνει το insert
                cmd.Parameters.AddWithValue("@RepTestID", c.RepTestID);
                cmd.Parameters.AddWithValue("@RightSum", c.RightSum);
                cmd.Parameters.AddWithValue("@WrongSum", c.WrongSum);
                cmd.Parameters.AddWithValue("@Prep1", c.Prep1);
                cmd.Parameters.AddWithValue("@Prep2", c.Prep2);
                cmd.Parameters.AddWithValue("@Prep3", c.Prep3);
                cmd.Parameters.AddWithValue("@Prep4", c.Prep4);
                cmd.Parameters.AddWithValue("@Prep5", c.Prep5);
                cmd.Parameters.AddWithValue("@Prep6", c.Prep6);
                cmd.Parameters.AddWithValue("@Prep7", c.Prep7);
                cmd.Parameters.AddWithValue("@Prep8", c.Prep8);
                cmd.Parameters.AddWithValue("@Prep9", c.Prep9);
                cmd.Parameters.AddWithValue("@UserID", c.UserID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //Αν το query τρέξει επιτυχώς τοτε η τιμή των γραμμών θα είναι μεγαλύτερη απο το 0 αλλιώς θα είναι 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
