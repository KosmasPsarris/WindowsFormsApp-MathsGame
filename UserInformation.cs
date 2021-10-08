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
    class UserInformation
    {
        //Getter Setter Εντολές
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int FirstLogIn { get; set; } //0 if first time, 1 if not

        string myconnstring = "Data Source=DESKTOP-QPGEM5G;Initial Catalog=EkpaideutikoLogismikoDB;Integrated Security=True";

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
                string sql = "Select * from UserInformation";
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

        //Select based on user απο βάση δεδομένων
        public DataTable SelectFromUser(UserInformation c)
        {

            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            //Προσωρινό table για να κρατάει τα δεδομένα απο την βάση
            DataTable dt = new DataTable();
            try
            {
                //Query βάσης για το select
                string sql = "SELECT * " +
                                "FROM UserInformation " +
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
        public bool Insert(UserInformation c)
        {
            //Η τιμή που θα γίνει return
            bool isSuccess = false;
            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Query βάσης για το insert
                string sql = "INSERT INTO UserInformation (UserID,Username,Password,FirstLogIn) VALUES " +
                    "(@UserID, @Username, @Password ,@FirstLogIn)";
                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Δημιουργία παραμέτρων για να γίνει το insert
                cmd.Parameters.AddWithValue("@UserID", c.UserID);
                cmd.Parameters.AddWithValue("@Username", c.Username);
                cmd.Parameters.AddWithValue("@Password", c.Password);
                cmd.Parameters.AddWithValue("@FirstLogIn", c.FirstLogIn);
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


        //Συνάρτηση για το update της βάσης 
        public bool Update(UserInformation c)
        {
            //Η τιμή που θα γίνει return
            bool isSuccess = false;
            //Σύνδεση με βάση
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Query βάσης για το update
                string sql = "UPDATE UserInformation " +
                  "SET UserID = @UserID, Username = @Username, Password = @Password, FirstLogIn = @FirstLogIn " +
                  "WHERE UserID = @UserID";

                //Comand χρησιμοποιώντας το conn και το sql
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Δημιουργία παραμέτρων για να γίνει το insert
                cmd.Parameters.AddWithValue("@UserID", c.UserID);
                cmd.Parameters.AddWithValue("@Username", c.Username);
                cmd.Parameters.AddWithValue("@Password", c.Password);
                cmd.Parameters.AddWithValue("@FirstLogIn", c.FirstLogIn);
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
