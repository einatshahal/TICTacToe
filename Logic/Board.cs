using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
     class Board
    {
        private readonly int m_BoardSize;
        private eCell[,]     m_Board;
        private int          m_FreeCells;

        public Board(int sizeBoard)
        {
            m_BoardSize = sizeBoard;
            m_FreeCells = sizeBoard * sizeBoard;
            m_Board = new eCell[m_BoardSize, m_BoardSize];
            ClearBoard();
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public int FreeCells
        {
            get { return m_FreeCells; }
        }

        public void ClearBoard()
        {
            m_FreeCells = m_BoardSize * m_BoardSize;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_Board[i, j] = eCell.EMPTY;
                }
            }
        }

        public bool IsCellEmpty(int i_row, int i_col)
        {
            bool isEmpty = true;

            if (m_Board[i_row, i_col] != eCell.EMPTY)
            {
                isEmpty = false;

            }
            return isEmpty;
        }

        public void UpdateCell(int i_row, int i_col, eCell i_playerSign)
        {
            m_Board[i_row, i_col] = i_playerSign;
            m_FreeCells = m_FreeCells - 1;
        }

        public string GetSignCell(int i_row, int i_col)
        {
           return m_Board[i_row, i_col].ToString();
        }
    }
}
