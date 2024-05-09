using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPlayer2.Checked)
            {
                this.textBoxPlayer2.Enabled = true;
            }
            else
            {
                this.textBoxPlayer2.ResetText();
                this.textBoxPlayer2.Enabled = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //play with another player
            if (checkBoxPlayer2.Checked == true)
            {
                GameForm newGame = new GameForm((int)numericUpDownRow.Value, textBoxPlayer1.Text, textBoxPlayer2.Text);
                newGame.ShowDialog();
            }
            //play with the computer
            else
            {
                GameForm newGame = new GameForm((int)numericUpDownRow.Value, textBoxPlayer1.Text);
                newGame.ShowDialog();
            }
            this.Close();
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownRow_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCol.Value = numericUpDownRow.Value;
        }

        private void numericUpDownCol_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRow.Value = numericUpDownCol.Value;
        }
    }
}
