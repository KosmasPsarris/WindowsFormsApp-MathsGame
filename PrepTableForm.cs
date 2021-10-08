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
    public partial class PrepTableForm : Form //This is the prep table form
    {
        public PrepTableForm()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Width = 600;
            this.Height = 500;
        }

        private void tablePictureBox_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm nextForm = new MenuForm();
            nextForm.ShowDialog();
            this.Close();
        }
    }
}
