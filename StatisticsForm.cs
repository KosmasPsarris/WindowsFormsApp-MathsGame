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
    public partial class StatisticsForm : Form
    {
        RepTest repTest = new RepTest();
        NormalTest normalTest = new NormalTest();


        public StatisticsForm()
        {
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;

            statisticsLabel.Text = "";
            statisticsRichTextBox.Text = "";
        }

        private void WinLoseButton_Click(object sender, EventArgs e)
        {
            statisticsLabel.Text = "Win / Lose Ratio";

            normalTest.UserID = LoginForm.userID; //1 = rightsum 2 = wrongsum indexes
            DataTable dataTable = normalTest.SelectFromUser(normalTest);

            int winCount = 0;
            int loseCount = 0;

            for(int i = 0; i < dataTable.Rows.Count; i++) //for each normal test/row
            { //find out if user won or lost
                if (int.Parse((string)dataTable.Rows[i].ItemArray[1]) < int.Parse((string)dataTable.Rows[i].ItemArray[2]))
                    loseCount++;
                else
                    winCount++;
            }

            float winPercent = winCount * 100 / (loseCount + winCount);
            float losePercent = loseCount * 100 / (loseCount + winCount);

            statisticsRichTextBox.Text = "Win Percent: "+ winPercent + " %\n" + "Lose Percent: " + losePercent + " %";
        }

        private void PrepImprovementButton_Click(object sender, EventArgs e)
        {
            statisticsLabel.Text = "Prep Improvement";

            repTest.UserID = LoginForm.userID; //3-11 indexes of preps
            DataTable dataTable = repTest.SelectFromUser(repTest);

            string resultString = "";

            //find max wrongs in a prep, if at the next test its different, then it means user has improved
            for (int i = 0; i < dataTable.Rows.Count - 1; i++) //for each normal test/row
            {
                int max1 = 0; //reset for each row
                int max2 = 0;
                int max1Count = -1;
                int max2Count = -1;

                for (int j = 3; j < 11; j++) //current row
                {
                    if (max1Count < (int)dataTable.Rows[i].ItemArray[j])
                    {
                        max1Count = (int)dataTable.Rows[i].ItemArray[j];
                        max1 = j - 2;
                    }
                }

                for (int k = 3; k < 11; k++) //next row
                {
                    if (max2Count < (int)dataTable.Rows[i+1].ItemArray[k])
                    {
                        max2Count = (int)dataTable.Rows[i+1].ItemArray[k];
                        max2 = k - 2;
                    }
                }

                string improvementResult = "";

                if (max1 == max2)
                    improvementResult = "You have not improved";
                else
                    improvementResult = "You have improved";

                if(max1 == 0 && max2 == 0)
                    resultString += "RepTest " + (i + 1) + "-" + (i + 2) + ": No mistakes.\n";
                else if(max1 == 0 && max2 != 0)
                    resultString += "RepTest " + (i + 1) + "-" + (i + 2) + ": Poor performance from " + max1 + " to " + max2 + "\n";
                else
                    resultString += "RepTest " + (i+1) + "-" + (i+2) + ": " + improvementResult + " from " + max1 + " to " + max2 + "\n";

            }

            if (resultString.Equals("")) //if there is only 1 rep test cant get results
                resultString = "Not enough Rep Tests!";

            statisticsRichTextBox.Text = resultString;
        }

        private void RepNormalRatioButton_Click(object sender, EventArgs e)
        {
            statisticsLabel.Text = "Rep / Normal Ratio";

            //Get the Count/number of normal and rep tests and find the percent
            repTest.UserID = LoginForm.userID;
            DataTable dataTableRep = repTest.SelectFromUser(repTest);

            normalTest.UserID = LoginForm.userID;
            DataTable dataTableNormal = normalTest.SelectFromUser(normalTest);

            float repPercent = dataTableRep.Rows.Count * 100 / (dataTableNormal.Rows.Count + dataTableRep.Rows.Count);
            float normalPercent = dataTableNormal.Rows.Count * 100 / (dataTableNormal.Rows.Count + dataTableRep.Rows.Count);

            statisticsRichTextBox.Text = "Normal Percent: " + normalPercent + " %\n" + "Rep Percent: " + repPercent + " %";
        }

        private void prepRationButton_Click(object sender, EventArgs e)
        {
            statisticsLabel.Text = "Prep Ratio";

            //get count for each prep (from 1 to 9) and find their percent
            repTest.UserID = LoginForm.userID;
            DataTable dataTable = repTest.SelectFromUser(repTest);
            
            //variables for counts
            int prep1 = 0;
            int prep2 = 0;
            int prep3 = 0;
            int prep4 = 0;
            int prep5 = 0;
            int prep6 = 0;
            int prep7 = 0;
            int prep8 = 0;
            int prep9 = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++) //for each normal test/row
            {
                prep1 += (int)dataTable.Rows[i].ItemArray[3];
                prep2 += (int)dataTable.Rows[i].ItemArray[4];
                prep3 += (int)dataTable.Rows[i].ItemArray[5];
                prep4 += (int)dataTable.Rows[i].ItemArray[6];
                prep5 += (int)dataTable.Rows[i].ItemArray[7];
                prep6 += (int)dataTable.Rows[i].ItemArray[8];
                prep7 += (int)dataTable.Rows[i].ItemArray[9];
                prep8 += (int)dataTable.Rows[i].ItemArray[10];
                prep9 += (int)dataTable.Rows[i].ItemArray[11];
            }

            int prepSum = prep1 + prep2 + prep3 + prep4 + prep5 + prep6 + prep7 + prep8 + prep9;

            //variables for percents
            float prep1Percent = prep1 * 100 / prepSum;
            float prep2Percent = prep2 * 100 / prepSum;
            float prep3Percent = prep3 * 100 / prepSum;
            float prep4Percent = prep4 * 100 / prepSum;
            float prep5Percent = prep5 * 100 / prepSum;
            float prep6Percent = prep6 * 100 / prepSum;
            float prep7Percent = prep7 * 100 / prepSum;
            float prep8Percent = prep8 * 100 / prepSum;
            float prep9Percent = prep9 * 100 / prepSum;

            statisticsRichTextBox.Text = "Prep1 Percent: " + prep1Percent + " %\n" +
                "Prep2 Percent: " + prep2Percent + " %\n" +
                "Prep3 Percent: " + prep3Percent + " %\n" +
                "Prep4 Percent: " + prep4Percent + " %\n" +
                "Prep5 Percent: " + prep5Percent + " %\n" +
                "Prep6 Percent: " + prep6Percent + " %\n" +
                "Prep7 Percent: " + prep7Percent + " %\n" +
                "Prep8 Percent: " + prep8Percent + " %\n" +
                "Prep9 Percent: " + prep9Percent + " %";
        }

        private void backPictureBox_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm nextForm = new MenuForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void statisticsInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Win/Lose Ratio: Ratio based on how many times the user won or lost the NormalTest race.\n\n" +
                        "Prep Improvement: Shows whether the user has had good or poor performance based on RepTest data.\n\n" +
                        "Prep Ratio: Ratio based on how many mistakes the user made for each prep number.\n\n" +
                        "Rep/Normal Ratio: Ratio based on how many times the user has undergone the Rep or Normal tests.");
        }
    }
}