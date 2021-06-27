using System;
using System.Threading;


namespace UI
{
    public class UserInterface
    {
        //private readonly GameSettingaForm settingsGui = new GameSettingaForm();
        //private readonly FormBoard boardGui = new FormBoard();
        private readonly Menu r_StartMenu = new Menu();
        public const int k_MaxSizeOfBoard = 9;
        public const int k_MinSizeOfBoard = 3;

        public void RunProgram()
        {
            
            //settingsGui.ShowDialog();

            //כפתור התחל == מודיע
            // מאזין - this.button1.Click
            //settingsGui.OnGameStarted();
            //boardGui.ShowDialog();

            bool ignoreable = false;
            int userBoardSizeChoice = 0;
            bool noFinalQuit = true, quit = false;
            int rowChosen, colChosen;
            Backend.Player currentPlayer, otherPlayer;

//            Console.WriteLine(
//            @"Hey and welcome to ^^^tic tac toe^^^
//please type the board size you would like (between 3 and 9)" + Environment.NewLine);
            userBoardSizeChoice = GetInputChoiceFromTheUser(ref ignoreable, out bool valid);
            BoardSizeInputValidtion(userBoardSizeChoice, ref ignoreable);

            Backend.Board gameBoard = new Backend.Board(userBoardSizeChoice);
            Console.Clear();
            r_StartMenu.SetStartMenu();
            r_StartMenu.ShowMenu();
            Backend.GameManager game = GetUserInitialChoiceForGameType(gameBoard, userBoardSizeChoice, ref ignoreable);
            if (game == null) /// לבדוק
            {
                noFinalQuit = false;
            }

            RefreshBoard(gameBoard);
            currentPlayer = game.m_player1;
            otherPlayer = game.m_player2;
            while (noFinalQuit)
            {
                //horani solution - i added ref to currentPlayer and to other Player because it doesnt save the change is playersinstance otherwise.
                RunRound(ref currentPlayer, ref otherPlayer, out rowChosen, out colChosen, gameBoard, out quit, ref noFinalQuit, userBoardSizeChoice, game);
            }

            Console.Clear();
            Console.WriteLine(game.EndGame());
            Thread.Sleep(5000);
        }

        public void RunRound(ref Backend.Player io_CurrentPlayer, ref Backend.Player io_OtherPlayer, out int o_RowChosen, out int o_ColChosen, Backend.Board gameBoard, out bool quit, ref bool io_NoFinalQuit, int i_UserBoardSizeChoice, Backend.GameManager i_Game)
        {
            GetPlayerCellChoice(io_CurrentPlayer, out o_RowChosen, out o_ColChosen, gameBoard, out quit, out bool numeric, i_Game);
            if (quit)
            {
                PromptOptionsDueToRoundEndAndGetChoice(i_Game, ref io_NoFinalQuit);
            }

            else if (i_Game.IsValidCell(o_RowChosen, o_ColChosen))
            {

                i_Game.MakeMove(o_RowChosen, o_ColChosen, io_CurrentPlayer.m_Sign);
                RefreshBoard(gameBoard);
                if (i_Game.CheckForSequence(io_CurrentPlayer.m_Sign))
                {
                    i_Game.SetPlayerWon(io_OtherPlayer);
                    if (gameBoard.IsFull())
                        Console.WriteLine("Full board! It's a TIE.");
                    Console.WriteLine(i_Game.GetScore());
                    PromptOptionsDueToRoundEndAndGetChoice(i_Game, ref io_NoFinalQuit);
                }

                else if (gameBoard.IsFull())
                {
                    Console.WriteLine("It's a TIE.");
                    Console.WriteLine(i_Game.GetScore());
                    PromptOptionsDueToRoundEndAndGetChoice(i_Game, ref io_NoFinalQuit);
                }

                Backend.GameManager.SwapTurns(ref io_CurrentPlayer, ref io_OtherPlayer);
            }

            else // invalid cell (out of bound OR already taken)
            {
                if (!io_CurrentPlayer.IsComputer())
                    Console.WriteLine("Invalid input. Please choose cell between 1,1 and {0},{0}", i_UserBoardSizeChoice);
            }
        }

        public void BoardSizeInputValidtion(int i_UserBoardSizeChoice, ref bool io_Ignoreable)
        {
            while (i_UserBoardSizeChoice > k_MaxSizeOfBoard || i_UserBoardSizeChoice < k_MinSizeOfBoard)
            {
                Console.Clear();
                if (QuitGameCheck(ref io_Ignoreable))
                {
                    Environment.Exit(1);
                }
                Console.Write("Please enter a number between 3 and 9" + Environment.NewLine);
                i_UserBoardSizeChoice = GetInputChoiceFromTheUser(ref io_Ignoreable, out bool valid);
            }
        }
        public bool QuitGameCheck(ref bool io_Quit)
        {
            if (io_Quit == true)
            {
                Console.WriteLine("quit game" + Environment.NewLine);
                Thread.Sleep(500);
            }

            return io_Quit;
        }


        public void RefreshBoard(Backend.Board i_GameBoard)
        {
            Console.Clear();
            Console.WriteLine(i_GameBoard.ToString()); //display board
        }

        public void PromptOptionsDueToRoundEndAndGetChoice(Backend.GameManager io_Game, ref bool io_ContinueGame)
        {
            Console.WriteLine("Dear Players,{0}Do you want to have another round?{0}If you do, please press 1.{0}If you wish to quit the game- press 2", Environment.NewLine);
            bool isNum = int.TryParse(Console.ReadLine(), out int userChoice);
            if (!isNum || (userChoice != 1 && userChoice != 2))
                Console.WriteLine("Invalid input.{0}", Environment.NewLine);
            else if (userChoice == 1)
            {
                io_Game.NewRound();
                RefreshBoard(io_Game.m_board);
            }
            else
            { // ==2
                io_ContinueGame = false;
            }
        }

        public void GetPlayerCellChoice(Backend.Player i_currentPlayer, out int o_Row, out int o_Col, Backend.Board i_GameBoard, out bool o_Quit, out bool o_Valid, Backend.GameManager i_Game)
        {
            o_Quit = false;
            o_Valid = true;

            if (!i_currentPlayer.IsComputer()) // Human player choice
            {
                Console.WriteLine("Please Choose point on board to set {0} in.{1}Insert row and coloumn, and then press ENTER.", i_currentPlayer.m_Sign, Environment.NewLine);
                o_Row = GetInputChoiceFromTheUser(ref o_Quit, out o_Valid);
                if (!o_Quit)
                    o_Col = GetInputChoiceFromTheUser(ref o_Quit, out bool valid);
                else
                    o_Col = 0;
            }

            else // Computer's random choice
            {
                i_Game.AIChooseComputerCordinates(out o_Row, out o_Col, i_GameBoard.GetNumOfRows(), i_GameBoard, (int)i_currentPlayer.m_Sign);
                Thread.Sleep(100);
            }
        }




        // Convert user number input to int
        public int GetInputChoiceFromTheUser(ref bool io_Quit, out bool o_CheckParsing)
        {
            io_Quit = false;
            string firstInputInCurrentTurn = Console.ReadLine();
            if (firstInputInCurrentTurn == "q")
                io_Quit = true;
            int o_UserChoice;
            o_CheckParsing = true;
            o_CheckParsing = int.TryParse(firstInputInCurrentTurn, out o_UserChoice);

            return o_UserChoice;
        }

        public Backend.GameManager GetUserInitialChoiceForGameType(Backend.Board i_GameBoard, int i_UserBoardSizeChoice, ref bool io_Quit)
        {
            Backend.GameManager game = null;
            bool validInput = false;  // SURELY VALID

            while (!validInput && !io_Quit)
            {
                int userGameTypeChoice = GetInputChoiceFromTheUser(ref io_Quit, out bool numeric);
                if (userGameTypeChoice != 1 && userGameTypeChoice != 2)
                {
                    Console.WriteLine("Please select a valid option.");
                }
                else //create game accordingly:
                {
                    validInput = true;
                    game = new Backend.GameManager((Backend.GameManager.eGameType)userGameTypeChoice, i_UserBoardSizeChoice, i_GameBoard);
                    Console.WriteLine(i_GameBoard.ToString()); //display board
                }
            }

            if (QuitGameCheck(ref io_Quit))
            {
                Environment.Exit(1);
            }

            return game;
        }
    }
}
