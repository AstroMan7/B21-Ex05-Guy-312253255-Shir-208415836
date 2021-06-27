using System;

namespace Backend
{
    public class GameManager
    {
        const bool v_PlayerIsComputer = true;
        private bool m_roundNotOver = true;
        public Backend.Board m_board { get; private set; }
        public Player m_player1 { get; set; }
        public Player m_player2 { get; set; }

        private Random m_RndAIchoice;
        private Random m_RndEdgesCol;
        private Random m_RndEdgesRow;

        public enum eGameType
        {
            P2P = 1, //player vs player
            P2C //player vs Computer
        }

        public GameManager(eGameType i_gameType, int i_boardSize, Backend.Board i_gameBoard, Player i_FirstPlayer, Player i_SecondPlayer = null)   // DEFAULT PARAM
        {
            m_board = i_gameBoard;
            m_player1 = i_FirstPlayer;
            if (i_gameType == eGameType.P2C)
            {
                m_player2 = new Player(Player.e_Sign.O, Player.e_InitialTurn.Second, v_PlayerIsComputer, "Computer");
                m_RndAIchoice = new Random();
                m_RndEdgesCol = new Random();
                m_RndEdgesRow = new Random();
            }
            else
                m_player2 = i_SecondPlayer;
        }

        public bool IsValidCell(int i_Row, int i_Col)
        {
            return m_board.IsInBound(i_Row, i_Col) && m_board.IsEmptyCell(i_Row, i_Col);
        }

        /*AI priority:
         * dont block other player
         * 
         */
        public void MakeMove(int i_Row, int i_Col, Player.e_Sign i_Sign)
        {
            m_board.SetCell(i_Row, i_Col, i_Sign);
        }

        public void SetPlayerWon(Player io_winningPlayer)   // Didn't get the idea of this
        {
            m_roundNotOver = false;
            io_winningPlayer.m_Points++;
        }

        public void NewRound()
        {
            m_board.ClearBoard();
        }

        public string EndGame()
        {
            string finaleString = "It was a pleasure, yalla bye." + Environment.NewLine;
            finaleString += this.GetScore();

            return finaleString;
        }

        private bool checkDiagonalSequence(Player.e_Sign i_SignToCheck)
        {
            bool isFullDiagonal = false;
            int countSignAppearences = 0;
            for (int i = 0; i < m_board.GetNumOfRows(); i++)
            {
                if (m_board.GetCellContent(i, i) == (int)i_SignToCheck)
                    countSignAppearences++;
            }

            if (countSignAppearences == m_board.GetNumOfRows())
                isFullDiagonal = true;
            else
            {
                countSignAppearences = 0;
                for (int i = m_board.GetNumOfRows() - 1, j = 0; i >= 0 && j < m_board.GetNumOfRows(); i--, j++)
                {
                    if (m_board.GetCellContent(j, i) == (int)i_SignToCheck)
                        countSignAppearences++;
                }

                if (countSignAppearences == m_board.GetNumOfRows())
                    isFullDiagonal = true;
            }

            return isFullDiagonal;
        }

        private bool checkHorizontalSequence(Player.e_Sign i_SignToCheck)
        {
            bool isFullRowOfSigns = false;
            int countSignAppearences = 0;
            for (int i = 0; i < m_board.GetNumOfRows(); i++)
            {
                for (int j = 0; j < m_board.GetNumOfRows(); j++)
                {
                    if (m_board.GetCellContent(i, j) == (int)i_SignToCheck)
                        countSignAppearences++;
                }

                if (countSignAppearences == m_board.GetNumOfRows())
                {
                    isFullRowOfSigns = true;
                    break;
                }

                else countSignAppearences = 0;
            }

            return isFullRowOfSigns;
        }

        private bool checkVerticalSequence(Player.e_Sign i_SignToCheck)
        {
            bool isFullColOfSigns = false;
            int countSignAppearences = 0;
            for (int i = 0; i < m_board.GetNumOfRows(); i++)
            {
                for (int j = 0; j < m_board.GetNumOfRows(); j++)
                {
                    if (m_board.GetCellContent(j, i) == (int)i_SignToCheck)
                        countSignAppearences++;
                }

                if (countSignAppearences == m_board.GetNumOfRows())
                {
                    isFullColOfSigns = true;
                    break;
                }

                else countSignAppearences = 0;
            }

            return isFullColOfSigns;
        }

        // I think it should e checked for a spesific e_Sign (X / O)
        // There's a chance that this all checking thing should be in board class
        public bool CheckForSequence(Player.e_Sign i_SignToCheck)
        {
            return checkHorizontalSequence(i_SignToCheck) || checkVerticalSequence(i_SignToCheck) || checkDiagonalSequence(i_SignToCheck);
        }

        public static void SwapTurns(ref Player io_PreviousPlayer, ref Player io_NewTurnPlayer)
        {
            Player tmpPlayer = io_PreviousPlayer;
            io_PreviousPlayer = io_NewTurnPlayer;
            io_NewTurnPlayer = tmpPlayer;
        }
        public void AIChooseComputerCordinates(out int o_Row, out int o_Col, int i_BoardSize, Backend.Board i_GameBoard)
        {
            bool hasChosenCell = false;

            o_Row = m_RndAIchoice.Next(1, i_BoardSize + 1);  // Maybe move to gamemanager or player
            o_Col = m_RndAIchoice.Next(1, i_BoardSize + 1);

            int compuersChoiceRow = m_RndEdgesRow.Next(2, i_BoardSize - 1);
            int computersChoiceCol = m_RndEdgesCol.Next(2, i_BoardSize - 1);
            if (i_GameBoard.IsEmptyCell(compuersChoiceRow, 1))
            {
                hasChosenCell = true;
                o_Row = compuersChoiceRow;
                o_Col = 1;
            }
            else if (i_GameBoard.IsEmptyCell(compuersChoiceRow, i_BoardSize))
            {
                hasChosenCell = true;
                o_Row = compuersChoiceRow;
                o_Col = i_BoardSize;
            }
            else if (i_GameBoard.IsEmptyCell(1, computersChoiceCol))
            {
                hasChosenCell = true;
                o_Row = 1;
                o_Col = computersChoiceCol;
            }
            else if (i_GameBoard.IsEmptyCell(i_BoardSize, computersChoiceCol))
            {
                hasChosenCell = true;
                o_Row = i_BoardSize;
                o_Col = computersChoiceCol;
            }

            int rowIndex = 1;
            while ((rowIndex <= i_BoardSize) && (!hasChosenCell))
            {
                if (i_GameBoard.IsEmptyRow(rowIndex))
                {
                    hasChosenCell = true;
                    o_Row = rowIndex;
                    o_Col = m_RndAIchoice.Next(1, i_BoardSize + 1); //row is empty, can rand a column
                }
                rowIndex++;
            }
            o_Col--;
            o_Row--;

        }

        public string GetScore()
        {
            string scoreString = "The Game Score, at this point, is:" + Environment.NewLine;
            scoreString += string.Format("Player 1 (plays with X) with: {0} points{1}Player 2 (plays with O) with: {2} points{3}"
                , m_player1.m_Points, Environment.NewLine, m_player2.m_Points, Environment.NewLine);

            return scoreString;
        }

    }
}
