using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkpaideutikoLogismikoREAL
{
    public partial class LoginForm : Form //This is the login form
    {
        UserInformation userInformationObject = new UserInformation();
        public static int userID;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;
        }

        //LOGIN BUTTON
        private void loginButton(object sender, EventArgs e)
        {
            bool userNotExists = false;

            //if user left a textbox empty or only with spaces, send error message
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
                MessageBox.Show("Please fill in both text boxes");
            else
            { //else get data with select query
                DataTable dataTable = userInformationObject.Select();

                //for each user check if username and password match from the given credentials
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (usernameTextBox.Text.Equals(dataTable.Rows[i].ItemArray[1])
                        && passwordTextBox.Text.Equals(dataTable.Rows[i].ItemArray[2]))
                    {
                        userNotExists = true;

                        if (dataTable.Rows[i].ItemArray[3].Equals(0)) //if user logins first time, show welcome/tutorial form instead of menu form
                        {
                            userID = (int)dataTable.Rows[i].ItemArray[0];

                            this.Hide();
                            WelcomeForm nextForm = new WelcomeForm();
                            nextForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            userID = (int)dataTable.Rows[i].ItemArray[0];

                            this.Hide();
                            MenuForm nextForm = new MenuForm();
                            nextForm.ShowDialog();
                            this.Close();
                        }
                    }
                }
                if(!userNotExists)
                    MessageBox.Show("User doesn't exist!");
            }
        }

        private void signupButton_Click_1(object sender, EventArgs e)
        {
            bool userAlreadyExists = false;

            //if user left a textbox empty or only with spaces, send error message
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
                MessageBox.Show("Please fill in both text boxes");
            else
            { //else get data with select query
                DataTable dataTable = userInformationObject.Select();

                //for each user check if username already exists, if not then sign up the new user
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (usernameTextBox.Text.Equals(dataTable.Rows[i].ItemArray[1]))
                        userAlreadyExists = true;
                }

                if (!userAlreadyExists)
                {
                    userInformationObject.UserID = dataTable.Rows.Count + 1;
                    userInformationObject.Username = usernameTextBox.Text;
                    userInformationObject.Password = passwordTextBox.Text;
                    userInformationObject.FirstLogIn = 0;


                    //Ελεγχος για την επιτυχια ή μη του insert
                    bool success = userInformationObject.Insert(userInformationObject);
                    if (success == true)
                    {
                        MessageBox.Show("User was SUCCESSFULY inserted to database.");
                    }
                    else
                    {
                        MessageBox.Show("User was NOT inserted to database.");
                    }
                }
                else
                    MessageBox.Show("Username already taken!");
            }
        }
    }
}
