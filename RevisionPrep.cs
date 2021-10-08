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
    public partial class RevisionPrep : Form
    {
        public RevisionPrep()
        {
            InitializeComponent();
        }

        private void backMenuPictureBox_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm nextForm = new MenuForm();
            nextForm.ShowDialog();
            this.Close();
        }

        private void RevisionPrep_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;

            //change image and label based on the problematic prep
            if (TestForm.prepToRevision == 1)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep1.png");
            else if (TestForm.prepToRevision == 2)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep2.png");
            else if (TestForm.prepToRevision == 3)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep3.png");
            else if (TestForm.prepToRevision == 4)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep4.png");
            else if (TestForm.prepToRevision == 5)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep5.png");
            else if (TestForm.prepToRevision == 6)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep6.png");
            else if (TestForm.prepToRevision == 7)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep7.png");
            else if (TestForm.prepToRevision == 8)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep8.png");
            else if (TestForm.prepToRevision == 9)
                revPrepPictureBox.BackgroundImage = Image.FromFile("Images/Multiplication table each prep/prep9.png");

            revPrepLabel.Text = "You had the most difficulty in the Prep of " + TestForm.prepToRevision;
        }
    }
}
