using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EkpaideutikoLogismikoREAL
{
    public partial class TestForm : Form //This is the form in which tests will take place
    {
        RepTest repTest = new RepTest();
        NormalTest normalTest = new NormalTest();
        UserInformation userInformationObject = new UserInformation();

        public static int prepToRevision;

        //sta aristera einai h propaidia
        int prepCounter;
        int currentPrepCounter; //so we won't have preps of numbers above 9
        int correctAnswer;
        Random random = new Random();

        int playerCarCounter = 0;
        int computerCarCounter = 0;

        //2 max to configure the questions
        int max1;
        int max2;

        List<int> incorrectAnswers = new List<int>();


        //RepTest DB variables
        int rightSum = 0;
        int wrongSum = 0;
        int prep1 = 0;
        int prep2 = 0;
        int prep3 = 0;
        int prep4 = 0;
        int prep5 = 0;
        int prep6 = 0;
        int prep7 = 0;
        int prep8 = 0;
        int prep9 = 0;

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            this.Width = 720;
            this.Height = 500;

            prepCounter = 1;

            if (MenuForm.isNormalTest == false) //user picked RepTest
            {

                userInformationObject.UserID = LoginForm.userID;
                DataTable dataTableUserInfo = userInformationObject.SelectFromUser(userInformationObject);

                if (dataTableUserInfo.Rows[0].ItemArray[3].Equals(0)) //if its the first login
                {
                    // change column of FirstLogIn to 1
                    userInformationObject.UserID = (int)dataTableUserInfo.Rows[0].ItemArray[0];
                    userInformationObject.Username = (string)dataTableUserInfo.Rows[0].ItemArray[1];
                    userInformationObject.Password = (string)dataTableUserInfo.Rows[0].ItemArray[2];
                    userInformationObject.FirstLogIn = 1;

                    //Ελεγχος για την επιτυχια ή μη του update
                    bool success = userInformationObject.Update(userInformationObject);
                    if (success == true)
                    {
                        MessageBox.Show("User was SUCCESSFULY updated to database.");
                    }
                    else
                    {
                        MessageBox.Show("User was NOT updated to database.");
                    }
                }

                FillTest();
            }
            else //user picked NormaLTest
            {
                max1 = 0; //reset
                max2 = 0;
                int max1Count = -1;
                int max2Count = -1;

                //get what id to put into RepTestID and find the latest reptest data
                repTest.UserID = LoginForm.userID;
                DataTable dataTable = repTest.SelectLatest(repTest);

                // 3 - 11 indexes are the preps from 1 to 9
                //find the last 2 max preps so we can configure the questions
                //first 6 question based on them
                //instert results to normaltest
                /*
                MessageBox.Show(dataTable.Rows[0].ItemArray[0].ToString()
                    + " " + dataTable.Rows[0].ItemArray[1]
                    + " " + dataTable.Rows[0].ItemArray[2]
                    + " " + dataTable.Rows[0].ItemArray[3]
                    + " " + dataTable.Rows[0].ItemArray[4]
                    + " " + dataTable.Rows[0].ItemArray[5]
                    + " " + dataTable.Rows[0].ItemArray[6]
                    + " " + dataTable.Rows[0].ItemArray[7]
                    + " " + dataTable.Rows[0].ItemArray[8]
                    + " " + dataTable.Rows[0].ItemArray[9]
                    + " " + dataTable.Rows[0].ItemArray[10]
                    + " " + dataTable.Rows[0].ItemArray[11]
                    + " " + dataTable.Rows[0].ItemArray[12]);
                    */

                //if both max are = then just introduce random questions
                //if one max is zero then introduce 6 questions of the non zero max
                //if both are not zero, introduce 3 questions each
                for(int i = 3; i < 11; i++)
                {
                    if(max1Count < (int)dataTable.Rows[0].ItemArray[i])
                    {
                        max1Count = (int)dataTable.Rows[0].ItemArray[i];
                        max1 = i - 2;
                    }
                }
                for (int i = 3; i < 11; i++)
                {
                    if (max2Count < (int)dataTable.Rows[0].ItemArray[i] && max1 != i - 2)
                    {
                        max2Count = (int)dataTable.Rows[0].ItemArray[i];
                        max2 = i - 2;
                    }
                }

                FillTest();
            }
        }

        private void answer1Button_Click(object sender, EventArgs e) //top-left
        {
            CheckUserAnswer(answer1Button.Text);
        }

        private void answer2Button_Click(object sender, EventArgs e) //bottom-left
        {
            CheckUserAnswer(answer2Button.Text);
        }

        private void answer3Button_Click(object sender, EventArgs e)  //top-right
        {
            CheckUserAnswer(answer3Button.Text);
        }

        private void answer4Button_Click(object sender, EventArgs e)  //bottom-right
        {
            CheckUserAnswer(answer4Button.Text);
        }


        //checks if the user answered correctly
        void CheckUserAnswer(string answer)
        {
            if (correctAnswer == int.Parse(answer))
            {
                playerCarPictureBox.Top -= 24;
                playerCarCounter++;

                rightSum++;
            }
            else
            {
                computerCarPictureBox.Top -= 24;
                computerCarCounter++;

                wrongSum++;

                if (currentPrepCounter == 1)
                    prep1++;
                else if (currentPrepCounter == 2)
                    prep2++;
                else if (currentPrepCounter == 3)
                    prep3++;
                else if (currentPrepCounter == 4)
                    prep4++;
                else if (currentPrepCounter == 5)
                    prep5++;
                else if (currentPrepCounter == 6)
                    prep6++;
                else if (currentPrepCounter == 7)
                    prep7++;
                else if (currentPrepCounter == 8)
                    prep8++;
                else if (currentPrepCounter == 9)
                    prep9++;
            }



            //check if player or computer won and end the test
            if (playerCarCounter == 11)
            {
                MessageBox.Show("player won");


                if (MenuForm.isNormalTest == false) //user picked RepTest then insert into RepTest Table the results
                {
                    //get what id to put into RepTestID
                    repTest.UserID = LoginForm.userID;
                    DataTable dataTable = repTest.SelectCount(repTest);

                    repTest.RepTestID = (int)dataTable.Rows[0].ItemArray[0] + 1;
                    repTest.RightSum = rightSum;
                    repTest.WrongSum = wrongSum;
                    repTest.Prep1 = prep1;
                    repTest.Prep2 = prep2;
                    repTest.Prep3 = prep3;
                    repTest.Prep4 = prep4;
                    repTest.Prep5 = prep5;
                    repTest.Prep6 = prep6;
                    repTest.Prep7 = prep7;
                    repTest.Prep8 = prep8;
                    repTest.Prep9 = prep9;

                    bool success = repTest.Insert(repTest);
                    if (success == true)
                        MessageBox.Show("RepTest Recorded");
                    else
                        MessageBox.Show("RepTest was NOT Recorded");

                    this.Hide();
                    MenuForm nextForm = new MenuForm();
                    nextForm.ShowDialog();
                    this.Close();
                }
                else //user picked NormalTest then insert into NormalTest Table the results
                {
                    //get what id to put into NormalTestID
                    normalTest.UserID = LoginForm.userID;
                    DataTable dataTable = normalTest.SelectCount(normalTest);

                    normalTest.NormalTestID = (int)dataTable.Rows[0].ItemArray[0] + 1;
                    normalTest.RightSum = rightSum.ToString();
                    normalTest.WrongSum = wrongSum.ToString();


                    bool success = normalTest.Insert(normalTest);
                    if (success == true)
                        MessageBox.Show("NormalTest Recorded");
                    else
                        MessageBox.Show("NormalTest was NOT Recorded");

                    this.Hide();
                    MenuForm nextForm = new MenuForm();
                    nextForm.ShowDialog();
                    this.Close();
                }
            }
            else if(computerCarCounter == 11)
            {
                MessageBox.Show("computer won");

                if (MenuForm.isNormalTest == false) //user picked RepTest then insert into RepTest Table the results
                {
                    //get what id to put into RepTestID
                    repTest.UserID = LoginForm.userID;
                    DataTable dataTable = repTest.SelectCount(repTest);

                    repTest.RepTestID = (int)dataTable.Rows[0].ItemArray[0] + 1;
                    repTest.RightSum = rightSum;
                    repTest.WrongSum = wrongSum;
                    repTest.Prep1 = prep1;
                    repTest.Prep2 = prep2;
                    repTest.Prep3 = prep3;
                    repTest.Prep4 = prep4;
                    repTest.Prep5 = prep5;
                    repTest.Prep6 = prep6;
                    repTest.Prep7 = prep7;
                    repTest.Prep8 = prep8;
                    repTest.Prep9 = prep9;
                    repTest.UserID = LoginForm.userID;

                    bool success = repTest.Insert(repTest);
                    if (success == true)
                        MessageBox.Show("RepTest Recorded");
                    else
                        MessageBox.Show("RepTest was NOT Recorded");

                    prepToRevision = FindMaxPrep();

                    if(prepToRevision == 0)  //if the max wrong prep is zero then there are no mistakes, so just return user to menu
                    {                      // else show prep table focused on the problematic prep number.
                        this.Hide();
                        MenuForm nextForm = new MenuForm();
                        nextForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        RevisionPrep nextForm = new RevisionPrep();
                        nextForm.ShowDialog();
                        this.Close();
                    }

                }
                else //user picked NormalTest then insert into NormalTest Table the results
                {
                    //get what id to put into NormalTestID
                    normalTest.UserID = LoginForm.userID;
                    DataTable dataTable = normalTest.SelectCount(normalTest);

                    normalTest.NormalTestID = (int)dataTable.Rows[0].ItemArray[0] + 1;
                    normalTest.RightSum = rightSum.ToString();
                    normalTest.WrongSum = wrongSum.ToString();


                    bool success = normalTest.Insert(normalTest);
                    if (success == true)
                        MessageBox.Show("NormalTest Recorded");
                    else
                        MessageBox.Show("NormalTest was NOT Recorded");

                    prepToRevision = FindMaxPrep();

                    if (prepToRevision == 0)  //if the max wrong prep is zero then there are no mistakes, so just return user to menu
                    {                      // else show prep table focused on the problematic prep number.
                        this.Hide();
                        MenuForm nextForm = new MenuForm();
                        nextForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        RevisionPrep nextForm = new RevisionPrep();
                        nextForm.ShowDialog();
                        this.Close();
                    }
                }
            }
            else //else continue test / race
                FillTest();
        }

        void FillTest()
        {
            incorrectAnswers.Clear(); //reset list

            if (MenuForm.isNormalTest == false) //reptest fill
            {
                if (prepCounter < 10) //if prep number is between 1 and 9 use it normally
                    currentPrepCounter = prepCounter;
                else                  //else pick randomly a normal prep number
                    currentPrepCounter = random.Next(1, 10);
            }
            else //normaltest fill
            {
                if(max1 == 0 && max2 == 0) //if there are no mistakes on previous reptest, copy reptest behavior 
                {
                    if (prepCounter < 10) //if prep number is between 1 and 9 use it normally
                        currentPrepCounter = prepCounter;
                    else                  //else pick randomly a normal prep number
                        currentPrepCounter = random.Next(1, 10);
                }
                else if(max1 == 0) //if one is zero (max1)
                {
                    if (prepCounter < 7) //put 6 questions of max2 first
                        currentPrepCounter = max2; 
                    else                  //the rest are random
                        currentPrepCounter = random.Next(1, 10);
                }
                else if(max2 == 0) //if one is zero (max2)
                {
                    if (prepCounter < 7) //put 6 questions of max1 first
                        currentPrepCounter = max1;
                    else                  //the rest are random
                        currentPrepCounter = random.Next(1, 10);
                }
                else //if both are non zero
                {
                    if (prepCounter < 4) //put 3 questions of max1 first then 3 of max2 then random
                        currentPrepCounter = max1;
                    else if (prepCounter > 3 && prepCounter < 7)
                        currentPrepCounter = max2;
                    else
                        currentPrepCounter = random.Next(1, 10);
                }
            }

            //get a random multiplier and create the question
            int randomMultiplier = random.Next(1, 10);
            questionLabel.Text = "The question is " + currentPrepCounter + " X " + randomMultiplier + ".";

            correctAnswer = currentPrepCounter * randomMultiplier; //find the answer

            bool whileBreaker; //this works as a listener to whether the list for loop has been broken or not
            bool whileBreaker2; //based on the first whileBreaker it breaks the while loop, that tries to find a new incorrect answer

            int randomedAnswer;
            //find 4 answers that are not correct for each button
            for (int i = 0; i < 4; i++)
            {
                while (true) //do while loop until a new incorrect answer has been found
                {
                    whileBreaker = true; //to break while loop
                    whileBreaker2 = false; //needs whileBreaker to to break while loop

                    randomedAnswer = currentPrepCounter * random.Next(1, 10); //random incorrect answer (can be the correct answer too)

                    if (randomedAnswer != correctAnswer) //if randomed answer is not the correct one
                    {
                        if (incorrectAnswers.Count != 0) //if there is at least one incorrect answer already in the list
                        {  //check for each element in the list if the incorrect answer is indeed new
                            for (int j = 0; j < incorrectAnswers.Count; j++)
                            {
                                if (randomedAnswer == incorrectAnswers[j]) //if its not new, break the for loop and find another incorrect answer
                                {
                                    whileBreaker = false; //answer not new so we won't break the while loop
                                    break;
                                }
                            }
                            //otherwise if the list has not the answer, it means that we can accept it
                            if (whileBreaker)
                                whileBreaker2 = true;

                            //allow whileBreaker2 to break the while loop
                            if (whileBreaker2)
                            {
                                //if it is new, then add it to list and break the while loop
                                incorrectAnswers.Add(randomedAnswer);
                                break;
                            }
                        }
                        else //if list is empty
                            incorrectAnswers.Add(randomedAnswer);
                    }
                }

            }

            //for each button, change their text to an incorrect answer
            for (int i = 0; i < incorrectAnswers.Count; i++)
            {
                if (i == 0)
                    answer1Button.Text = incorrectAnswers[i].ToString();
                else if (i == 1)
                    answer2Button.Text = incorrectAnswers[i].ToString();
                else if (i == 2)
                    answer3Button.Text = incorrectAnswers[i].ToString();
                else if (i == 3)
                    answer4Button.Text = incorrectAnswers[i].ToString();
            }

            //put the correct answer to a random bottom
            int correctButtonAnswer = random.Next(1, 5); //from 1 to 4

            if (correctButtonAnswer == 1)
            {
                answer1Button.Text = correctAnswer.ToString();
            }
            else if (correctButtonAnswer == 2)
            {
                answer2Button.Text = correctAnswer.ToString();
            }
            else if (correctButtonAnswer == 3)
            {
                answer3Button.Text = correctAnswer.ToString();
            }
            else if (correctButtonAnswer == 4)
            {
                answer4Button.Text = correctAnswer.ToString();
            }

            prepCounter++;
        }

        int FindMaxPrep() //find max prep
        {
            int maxPrep = 0;
            int maxPrepCounter = -1;

            if(maxPrepCounter < prep1)
            {
                maxPrepCounter = prep1;
                maxPrep = 1;
            }
            if (maxPrepCounter < prep2)
            {
                maxPrepCounter = prep2;
                maxPrep = 2;
            }
            if (maxPrepCounter < prep3)
            {
                maxPrepCounter = prep3;
                maxPrep = 3;
            }
            if (maxPrepCounter < prep4)
            {
                maxPrepCounter = prep4;
                maxPrep = 4;
            }
            if (maxPrepCounter < prep5)
            {
                maxPrepCounter = prep5;
                maxPrep = 5;
            }
            if (maxPrepCounter < prep6)
            {
                maxPrepCounter = prep6;
                maxPrep = 6;
            }
            if (maxPrepCounter < prep7)
            {
                maxPrepCounter = prep7;
                maxPrep = 7;
            }
            if (maxPrepCounter < prep8)
            {
                maxPrepCounter = prep8;
                maxPrep = 8;
            }
            if (maxPrepCounter < prep9)
            {
                maxPrep = 9;
            }

            return maxPrep;
        }
    }
}
