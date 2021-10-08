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
    class NormalTest
    {
        //Getter Setter Εντολές
        public int NormalTestID { get; set; }
        public string RightSum { get; set; }
        public string WrongSum { get; set; }
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
                string sql = "Select * from NormalTest";
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
        public DataTable SelectCount(NormalTest c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT COUNT(*) " +
                                "FROM NormalTest " +
                                "INNER JOIN UserInformation ON NormalTest.UserID = UserInformation.UserID " +
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

        //Select all based on user απο βάση δεδομένων
        public DataTable SelectFromUser(NormalTest c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT * " +
                                "FROM NormalTest " +
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
        public bool Insert(NormalTest c)
        {
            //Η τιμή που θα γίνει return
            bool isSuccess = false;
            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Query βάσης για το insert
                string sql = "INSERT INTO NormalTest (NormalTestID,RightSum,WrongSum,UserID) VALUES (@NormalTestID,@RightSum, @WrongSum, @UserID)";
                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Δημιουργία παραμέτρων για να γίνει το insert
                cmd.Parameters.AddWithValue("@NormalTestID", c.NormalTestID);
                cmd.Parameters.AddWithValue("@RightSum", c.RightSum);
                cmd.Parameters.AddWithValue("@WrongSum", c.WrongSum);
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