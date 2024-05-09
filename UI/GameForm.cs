using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace UI
{
    public partial class GameForm : Form
    {
        private MangeGame m_Game = new MangeGame();
        private int       m_BoardSize;
        public Button[,]  m_BoardButtons;
        private string    m_Player1Name;
        private string    m_Player2Name;

        public GameForm(int i_BoradSize, string i_NamePlayer1, string i_NamePlayer2 = null)
        {
            InitializeComponent();
            m_BoardButtons = new Button[i_BoradSize, i_BoradSize];
            initFormGame(i_BoradSize, i_NamePlayer1, i_NamePlayer2);
            createButtonsBorad();
            updateGameForm();
        }

        public void initFormGame(int i_BoradSize, string i_NamePlayer1, string i_NamePlayer2 = null)
        {
            m_BoardSize = i_BoradSize;
            m_Player1Name = i_NamePlayer1;
            if (i_NamePlayer2 == null)
            {
                m_Player2Name = "Computer";
            }
            else
            {
                m_Player2Name = i_NamePlayer2;
            }
            m_Game.InitBoard(i_BoradSize);
            m_Game.UpdateDetailsForNewGame();
            this.labelPlayer1.Text = m_Player1Name + ":" + m_Game.Player1Score();
            this.labelPlayer2.Text = m_Player2Name + ":" + m_Game.Player2Score();
        }

        public void createButtonsBorad()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_BoardButtons[i, j] = new Button();
                    m_BoardButtons[i, j].Height = 60;
                    m_BoardButtons[i, j].Width = 80;
                    m_BoardButtons[i, j].Click += ButtonsBoard_Clicked;
                    panelBoard.Controls.Add(m_BoardButtons[i, j]);
                    m_BoardButtons[i, j].Location = new Point(i * 85, j * 65);
                    m_BoardButtons[i, j].BackColor = Color.White;
                    m_BoardButtons[i, j].Enabled = true;
                    m_BoardButtons[i, j].Text = " ";
                }
            }
        }

        public void updateButtonsBorad()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_BoardButtons[i, j].BackColor = Color.White;
                    m_BoardButtons[i, j].Enabled = true;
                    m_BoardButtons[i, j].Text = " ";
                }
            }
        }

        public void updateGameForm()
        {
            this.labelPlayer1.Text = m_Player1Name + ":" + m_Game.Player1Score();
            this.labelPlayer2.Text = m_Player2Name + ":" + m_Game.Player2Score();
            for (int k = 0; k < m_BoardSize; k++)
            {
                for (int h = 0; h < m_BoardSize; h++)
                {
                    updateButton(k, h);
                }
            }
        }

        private void updateButton(int x, int y)
        {
            switch (m_Game.GetSignCell(x, y))
            {
                case " ":
                    m_BoardButtons[x, y].Text = " ";
                    break;
                case "O":
                    m_BoardButtons[x, y].Text = eCell.O.ToString();
                    m_BoardButtons[x, y].Enabled = false;
                    m_BoardButtons[x, y].BackColor = Color.LightGray;
                    break;
                case "X":
                    m_BoardButtons[x, y].Text = eCell.X.ToString();
                    m_BoardButtons[x, y].Enabled = false;
                    m_BoardButtons[x, y].BackColor = Color.LightGray;
                    break;
            }
        }   

        public void checkIfGameFinish(int x, int y)
        {
            if (m_Game.IsGameFinish(x, y))
            {
                string i_message;
                string i_messageHead;
                if (m_Game.IsTie)
                {
                    i_message = "Tie!\nWould you like to play another round?";
                    i_messageHead = "A tie!";
                }
                else if (m_Game.isPlayer1Win)
                {
                    i_messageHead = "A Win!";
                    i_message = "The winner is " + m_Player1Name + "\nWould you like to play another round?";
                }
                else
                {
                    i_messageHead = "A Win!";
                    i_message = "The winner is " + m_Player2Name + "\nWould you like to play another round?";
                }
                if (MessageBox.Show(i_message, i_messageHead, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    initFormGame(m_BoardSize, m_Player1Name, m_Player2Name);
                    updateButtonsBorad();
                    updateGameForm();
                    this.Refresh();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void ButtonsBoard_Clicked(object sender, EventArgs e)
        {
            Button buttonClicked = (Button)sender;
            int x = buttonClicked.Location.X / 85;
            int y = buttonClicked.Location.Y / 65;
            if (m_Game.IsEmptyCell(x, y))
            {
                m_Game.UpdateCell(x, y);
                updateButton(x, y);
                checkIfGameFinish(x, y);
                updateGameForm();
                m_Game.switchTurn();
                this.Refresh();
            }
            if (m_Player2Name == "Computer")
            {
                System.Threading.Thread.Sleep(100);
                m_Game.UpdateComputerMove(out x, out y);
                updateButton(x, y);
                checkIfGameFinish(x, y);
                updateGameForm();
                m_Game.switchTurn();
                this.Refresh();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
