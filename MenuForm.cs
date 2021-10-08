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
    public partial class MenuForm : Form //This is the menu form of the application
    {
        public static bool isNormalTest; //is its false it means we are doing RepTest, otherwise normalTest

        UserInformation userInformationObject = new UserInformation();

        public MenuForm()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;

            userInformationObject.UserID = LoginForm.userID;
            DataTable dataTable = userInformationObject.SelectFromUser(userInformationObject);

            if (dataTable.Rows[0].ItemArray[3].Equals(0)) //if its the first login
            {
                //disable/hide all buttons and arrows and only show RepTest option
                DisableControls();
            }
            else //else show and enable everything
            {
                EnableControls();
            }

        }

        void EnableControls() //show everything
        {
            logoutButton.Enabled = true;
            logoutButton.Visible = true;
            normalButton.Enabled = true;
            normalButton.Visible = true;
            repButton.Enabled = true;
            repButton.Visible = true;
            statisticsButton.Enabled = true;
            statisticsButton.Visible = true;
            PrepButton.Enabled = true;
            PrepButton.Visible = true;

            NormalPictureBox.Enabled = false;
            NormalPictureBox.Visible = false;
            prepPictureBox.Enabled = false;
            prepPictureBox.Visible = false;
            RepPictureBox.Enabled = false;
            RepPictureBox.Visible = false;
            statisticsPictureBox.Enabled = false;
            statisticsPictureBox.Visible = false;
        }

        void DisableControls() //show only rep and Prep buttons and arrows
        {
            normalButton.Enabled = false;
            normalButton.Visible = false;
            repButton.Enabled = true;
            repButton.Visible = true;
            statisticsButton.Enabled = false;
            statisticsButton.Visible = false;
            PrepButton.Enabled = true;
            PrepButton.Visible = true;

            NormalPictureBox.Enabled = false;
            NormalPictureBox.Visible = false;
            prepPictureBox.Enabled = true;
            prepPictureBox.Visible = true;
            RepPictureBox.Enabled = true;
            RepPictureBox.Visible = true;
            statisticsPictureBox.Enabled = false;
            statisticsPictureBox.Visible = false;
        }

        private void repButton_Click(object sender, EventArgs e) //Revisional Test
        {
            isNormalTest = false;

            this.Hide();
            TestForm nextForm = new TestForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void normalButton_Click(object sender, EventArgs e) //Normal Test
        {
            isNormalTest = true;

            this.Hide();
            TestForm nextForm = new TestForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm nextForm = new LoginForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void PrepButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            PrepTableForm nextForm = new PrepTableForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void statisticsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatisticsForm nextForm = new StatisticsForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void menuInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RepTest: Revisional test that includes random prep questions.\n\n" +
                "NormalTest: Standard test that takes into account the weaknesses of the user.\n\n" +
                "Statistics: Shows information based on the data of the logged in user.\n\n" +
                "PrepTable: Shows the prep table so the user can study it.\n\n" +
                "LogOut: Log out of the application.");
        }
    }
}
