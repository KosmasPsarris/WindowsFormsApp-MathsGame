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
    public partial class WelcomeForm : Form  //This is the noob welcoming form between login form and menu form, shown only once
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;
        }

        private void welcomePictureBox_Click(object sender, EventArgs e)
        {
            this.Hide();
            PrepTableForm nextForm = new PrepTableForm();
            nextForm.ShowDialog();
            this.Close();
        }
    }
}
