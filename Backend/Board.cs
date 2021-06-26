using System;

namespace Backend
{   
    //HI
    public class Board
    {
        private int m_NumOfRowsAndCols;
        private int[,] m_BoardMatrix;


        public Board(int i_NumOfRowsAndCols)
        {
            m_BoardMatrix = new int[i_NumOfRowsAndCols, i_NumOfRowsAndCols];
            m_NumOfRowsAndCols = i_NumOfRowsAndCols;
        }

        public int GetNumOfRows()
        {
            return m_NumOfRowsAndCols;
        }

        public bool IsFull()
        {
            bool isFull = true;
            foreach (int cell in m_BoardMatrix)
            {
                if (cell == (int)Player.e_Sign.Empty)
                {
                    isFull = false;
                    break;
                }
            }
            return isFull;
        }

        public bool IsEmptyRow(int i_Row)
        {
            bool emptyRow = true;
            for (int i = 1; i <= GetNumOfRows(); i++) //run over columns
            {
                if (!IsEmptyCell(i_Row, i))
                {
                    emptyRow = false;
                }
            }
            return emptyRow;
        }

        public bool IsEmptyCell(int i_Row, int i_Col)
        {
            return m_BoardMatrix[i_Row - 1, i_Col - 1] == (int)Player.e_Sign.Empty;
        }

        public int GetCellContent(int i_Row, int i_Col)
        {
            return m_BoardMatrix[i_Row, i_Col];
        }

        public void SetCell(int i_Row, int i_Col, Player.e_Sign i_Value)
        {
            m_BoardMatrix[i_Row - 1, i_Col - 1] = (int)i_Value;
        }

        public override string ToString()
        {
            string tostring = "  ";
            for (int i = 1; i <= m_NumOfRowsAndCols; i++)
            {
                tostring += string.Format("{0}   ", i);
            }

            tostring += Environment.NewLine;
            for (int j = 1; j <= m_NumOfRowsAndCols; j++)
            {
                tostring += string.Format("{0}|", j);
                for (int i = 1; i <= m_NumOfRowsAndCols; i++)
                {
                    if (m_BoardMatrix[j - 1, i - 1] == (int)Player.e_Sign.Empty)
                        tostring += "   |";
                    else
                        tostring += string.Format(" {0} |", (Player.e_Sign)m_BoardMatrix[j - 1, i - 1]);
                }

                tostring += Environment.NewLine;
                tostring += " =";
                for (int i = 0; i < m_NumOfRowsAndCols; i++)
                {
                    tostring += "====";
                }
                tostring += Environment.NewLine;
            }
            return tostring;
        }

        public bool IsInBound(int i_Row, int i_Col)
        {
            return (i_Row <= m_NumOfRowsAndCols) && (i_Row >= 1) && (i_Col) >= 1 && (i_Col <= m_NumOfRowsAndCols);
        }

        public void ClearBoard()
        {
            for (int i = 0; i < m_NumOfRowsAndCols; i++)
            {
                for (int j = 0; j < m_NumOfRowsAndCols; j++)
                {
                    m_BoardMatrix[i, j] = 0;
                }
            }
        }

    }
}
