using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class MangeGame
    {
        private Board  m_Board;
        private Player m_Player1 = new Player();
        private Player m_Player2 = new Player();
        private bool   m_IsPlayer1Turn = true;
        private bool   m_IsTie = true;
        private bool   m_IsPlayer1Win;
        
        public static void Main()
        { }

        public bool IsPlayer1Turn
        {
            get { return m_IsPlayer1Turn; }
            set { m_IsPlayer1Turn = value; }
        }

        public bool IsPlayer2Computer
        {
            get { return m_Player2.IsComputer; }
        }

        public bool IsTie
        {
            get { return m_IsTie; }
        }

        public bool isPlayer1Win
        {
            get { return m_IsPlayer1Win; }
        }

        public int Player1Score()
        {
            return m_Player1.Score;
        }

        public int Player2Score()
        {
            return m_Player2.Score;
        }

        public void InitBoard(int i_sizeBoard)
        {
            m_Board = new Board(i_sizeBoard);
            m_Player2.PlayerSign = eCell.O;
            m_Player1.PlayerSign = eCell.X;
        }

        public void InitPlayers(int i_numberChoise)
        {
            m_Player1.PlayerSign = eCell.X;
            m_Player2.PlayerSign = eCell.O;
            m_Player1.IsComputer = false;
            m_IsTie = true;
            if (i_numberChoise == 1)
            {
                m_Player2.IsComputer = false;
            }
            if (i_numberChoise == 2)
            {
                m_Player2.IsComputer = true;
            }
        }

        public void UpdateCell(int i_row,int i_col)
        {
            if (m_IsPlayer1Turn == true)
            {
                m_Board.UpdateCell(i_row, i_col, m_Player1.PlayerSign);
            }
            else
            {
                m_Board.UpdateCell(i_row, i_col, m_Player2.PlayerSign);
            }
        }

        public void switchTurn()
        {
            if (m_IsPlayer1Turn)
            {
                m_IsPlayer1Turn = false;
            }
            else
            {
                m_IsPlayer1Turn = true;
            }
        }

        public void UpdateComputerMove(out int x, out int y)
        {
            Random rnd = new Random();
            x = rnd.Next(0, m_Board.BoardSize - 1);
            y = rnd.Next(0, m_Board.BoardSize - 1);
            while(m_Board.GetSignCell(x, y) != eCell.EMPTY.ToString())
            {
                x = rnd.Next(0, m_Board.BoardSize);
                y = rnd.Next(0, m_Board.BoardSize);
            }
            m_Board.UpdateCell(x, y, m_Player2.PlayerSign);
        }

        public bool IsEmptyCell(int i_row,int i_col)
        {
            bool isEmpty = true;
            if(!m_Board.IsCellEmpty(i_row,i_col))
            {
                isEmpty = false;
            }
            return isEmpty;
        }

        public string GetSignCell(int i_row,int i_col)
        {
            string sign = m_Board.GetSignCell(i_row, i_col);
            if (sign == "EMPTY")
            {
                sign = " ";
            }
            return sign;
        }

        public bool IsPlayerLose(int i_row, int i_col)
        {
            bool  IsLose = true;
            eCell signPlayer;
            bool  rowMatch = false; 
            bool  colMatch = false;
            bool  mainDiagonalMatch = false;
            bool  secondaryDiagonalMatch = false;

            if (m_IsPlayer1Turn == true)
            {
                signPlayer = m_Player1.PlayerSign;
            }
            else
            {
                signPlayer = m_Player2.PlayerSign;
            }
            //check row
            for (int i = 0; i < m_Board.BoardSize; i++) 
            {
                if (m_Board.GetSignCell(i_row,i) != signPlayer.ToString())
                {
                    IsLose = false;
                    break;
                }
            }
            if (IsLose)
            {
                rowMatch = true;
            }
            //check col
            IsLose = true;
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                if(m_Board.GetSignCell(i, i_col) != signPlayer.ToString())
                {
                    IsLose = false;
                    break;
                }
            }
            if (IsLose)
            {
                colMatch = true;
            }
            //slant1
            IsLose = true;
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                if(m_Board.GetSignCell(i, i) != signPlayer.ToString())
                {
                    IsLose = false;
                    break;
                }
            }
            if (IsLose)
            {
                mainDiagonalMatch = true;
            }
            //slant2
            int tempCol = m_Board.BoardSize-1;
            IsLose = true;
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                if (m_Board.GetSignCell(i, tempCol) != signPlayer.ToString())
                {
                    IsLose = false;
                    break;
                }
                tempCol--;
            }

            if (IsLose)
            {
                secondaryDiagonalMatch = true;
            }
            //End of the game
            if (secondaryDiagonalMatch || mainDiagonalMatch || colMatch || rowMatch)
            {
                if (m_IsPlayer1Turn == true)
                {
                    m_Player2.Score++;
                    m_IsPlayer1Win = false; //player2 is Win
                }
                else
                {
                    m_Player1.Score++;
                    m_IsPlayer1Win = true; //player1 is Win
                }
                m_IsTie = false;
            }
            return (secondaryDiagonalMatch || mainDiagonalMatch || colMatch || rowMatch);
        }

        public bool IsGameFinish(int i_row, int i_col)
        {
            bool gameFinish = false;

            if (m_Board.FreeCells == 0)
            {
                gameFinish = true; //the game is finish
                IsPlayerLose(i_row, i_col); //check who is the winner
            }
            else
            {
                gameFinish = IsPlayerLose(i_row, i_col); //check if there any winner
            }
            return gameFinish;
        }

        public void UpdateDetailsForNewGame()
        {
            m_Board.ClearBoard();
            m_IsPlayer1Win = false;
            m_IsTie = true;
        }
    }
}

