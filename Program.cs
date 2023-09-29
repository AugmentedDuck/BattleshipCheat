namespace BattleshipCheat
{
    internal class Program
    {
        static string[,] CreateBoardString(int boardSize)
        {
            string[,] board = new string[boardSize, boardSize];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = " ";
                }
            }

            return board;
        }

        static int[,] CreateBoardInt(int boardSize)
        {
            int[,] board = new int[boardSize, boardSize];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }

            return board;
        }

        static string OutputBoardState(int[,] currentBoard)
        {
            string boardString = string.Empty;

            for (int i = 0; i < currentBoard.GetLength(0); i++)
            {
                boardString += "|";

                for (int j = 0; j < currentBoard.GetLength(1); j++)
                {
                    boardString += " " + currentBoard[j, i] + " |";
                }

                boardString += "\n";
            }

            return boardString;
        }

        static string OutputBoardState(string[,] currentBoard)
        {
            string boardString = string.Empty;

            for (int i = 0; i < currentBoard.GetLength(0); i++)
            {
                boardString += "|";

                for (int j = 0; j < currentBoard.GetLength(1); j++)
                {
                    boardString += " " + currentBoard[j, i] + " |";
                }

                boardString += "\n";
            }

            return boardString;
        }

        static void Main(string[] args)
        {
            string[,] shownBoard = CreateBoardString(10);
            int[,] debugBoard = CreateBoardInt(10);
            bool[] isShipLeft = { true, true, true, true, true };
            int numberOfShots = 0;

            while (isShipLeft[0] && isShipLeft[1] && isShipLeft[2] && isShipLeft[3] && isShipLeft[4])
            {
                numberOfShots++;
                Console.WriteLine(OutputBoardState(shownBoard));

                int[] bestMove = CalculateBestMove(debugBoard, isShipLeft);

                Console.WriteLine((bestMove[0] + 1) + " ; " + (bestMove[1] + 1));

                string answer = Console.ReadLine();
                if (answer == "h")
                {
                    int[,] newBoard = HasBeenHit(debugBoard, bestMove[0], bestMove[1]);
                    int shipSize = 0;
                    for (int i = 0; i < newBoard.GetLength(0); i++)
                    {
                        for (int j = 0; j < newBoard.GetLength(1); j++)
                        {
                            if (newBoard[i,j] == 1 && debugBoard[i,j] == 0)
                            {
                                debugBoard[i, j] = 1;
                                shownBoard[i, j] = "O";
                            } else if (newBoard[i,j] == 2)
                            {
                                debugBoard[i, j] = 1;
                                shownBoard[i, j] = "X";
                                shipSize++;
                            }
                        }
                    }

                    switch (shipSize)
                    {
                        case 2:
                            isShipLeft[0] = false;
                            break;
                        case 3:
                            if (isShipLeft[1])
                            {
                                isShipLeft[1] = false;
                            }
                            else
                            {
                                isShipLeft[2] = false;
                            }
                            break;
                        case 4:
                            isShipLeft[3] = false;
                            break;
                        case 5:
                            isShipLeft[4] = false;
                            break;
                        default:
                            break;
                    }
                }
                else if (answer == "m")
                {
                    debugBoard[bestMove[0], bestMove[1]] = 1;
                    shownBoard[bestMove[0], bestMove[1]] = "O";
                }

            }

            Console.WriteLine($"All ships sunk\n I used {numberOfShots} shots, thats a hit rate of: {MathF.Round((17 / numberOfShots) * 100, 2)}%");
        }

        private static int[,] HasBeenHit(int[,] board, int x, int y)
        {
            string answer = " ";
            int[,] newBoard = board;
            newBoard[x, y] = 2;
            bool isGoingUp = true;
            bool isGoingDown = true;
            bool isGoingRight = true;
            bool isGoingLeft = true;
            int goingUp = 0;
            int goingDown = 0;
            int goingLeft = 0;
            int goingRight = 0;


            while (answer != "s")
            {
                if (y > 0 && isGoingUp)
                {
                    goingUp++;
                    y--;
                    Console.WriteLine((x + 1) + " ; " + (y + 1));
                    answer = Console.ReadLine();
                    
                    if (answer == "h" || answer == "s")
                    {
                        newBoard[x, y] = 2;
                        isGoingRight = false;
                        isGoingLeft = false;
                    } else if (answer == "m")
                    {
                        newBoard[x, y] = 1;
                        isGoingUp = false;
                        y += goingUp;
                    }
                }
                else if (y < board.GetLength(1) && isGoingDown)
                {
                    goingDown++;
                    y++;
                    Console.WriteLine((x + 1) + " ; " + (y + 1));

                    answer = Console.ReadLine();

                    if (answer == "h" || answer == "s")
                    {
                        newBoard[x, y] = 2;
                        isGoingRight = false;
                        isGoingLeft = false;
                    }
                    else if (answer == "m")
                    {
                        newBoard[x, y] = 1;
                        isGoingDown = false;
                        y -= goingDown;
                    }
                }
                else if (x > 0 && isGoingLeft)
                {
                    goingLeft++;
                    x--;
                    Console.WriteLine((x + 1) + " ; " + (y + 1));

                    answer = Console.ReadLine();

                    if (answer == "h" || answer == "s")
                    {
                        newBoard[x, y] = 2;
                        isGoingUp = false;
                        isGoingDown = false;
                    }
                    else if (answer == "m")
                    {
                        newBoard[x, y] = 1;
                        isGoingLeft = false;
                        x += goingLeft;
                    }
                }
                else if (x < board.GetLength(1) && isGoingRight)
                {
                    goingRight++;
                    x++;
                    Console.WriteLine((x + 1) + " ; " + (y + 1));
                    answer = Console.ReadLine();

                    if (answer == "h" || answer == "s")
                    {
                        newBoard[x, y] = 2;
                        isGoingUp = false;
                        isGoingDown = false;
                    }
                    else if (answer == "m")
                    {
                        newBoard[x, y] = 1;
                        isGoingRight = false;
                        x -= goingRight;
                    }
                }
            }

            return newBoard;
        }

        static int[] CalculateBestMove(int[,] board, bool[] isShipLeft)
        {
            int[] bestMove = new int[2];

            int[,] boardTwo = BestMoveTwo(board);
            int[,] boardThree = BestMoveThree(board);
            int[,] boardFour = BestMoveFour(board);
            int[,] boardFive = BestMoveFive(board);

            int bestScore = 0;

            int[,] scoreBoard = new int[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    scoreBoard[i, j] = 0;
                    int currentScore = 0;

                    if (isShipLeft[0])
                    {
                        scoreBoard[i, j] += boardTwo[i, j];
                        currentScore += boardTwo[i, j];
                    }

                    if (isShipLeft[1])
                    {
                        scoreBoard[i, j] += boardThree[i, j];
                        currentScore += boardThree[i, j];
                    }

                    if (isShipLeft[2])
                    {
                        scoreBoard[i, j] += boardThree[i, j];
                        currentScore += boardThree[i, j];
                    }

                    if (isShipLeft[3])
                    {
                        scoreBoard[i, j] += boardFour[i, j];
                        currentScore += boardFour[i, j];
                    }

                    if (isShipLeft[4])
                    {
                        scoreBoard[i, j] += boardFive[i, j];
                        currentScore += boardFive[i, j];
                    }

                    if (currentScore > bestScore)
                    {
                        bestScore = currentScore;
                        bestMove[0] = i;
                        bestMove[1] = j;
                    }
                }
            }

            Console.WriteLine(OutputBoardState(scoreBoard));
            return bestMove;
        }

        private static int[,] BestMoveFive(int[,] board)
        {
            int[,] scoreBoard = new int[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i < board.GetLength(0) - 4)
                    {
                        if (board[j, i] == 0 && board[j, i + 1] == 0 && board[j, i + 2] == 0 && board[j, i + 3] == 0 && board[j, i + 4] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j, i + 1]++;
                            scoreBoard[j, i + 2]++;
                            scoreBoard[j, i + 3]++;
                            scoreBoard[j, i + 4]++;
                        }
                    }

                    if (j < board.GetLength(1) - 4)
                    {
                        if (board[j, i] == 0 && board[j + 1, i] == 0 && board[j + 2, i] == 0 && board[j + 3, i] == 0 && board[j + 4, i] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j + 1, i]++;
                            scoreBoard[j + 2, i]++;
                            scoreBoard[j + 3, i]++;
                            scoreBoard[j + 4, i]++;
                        }
                    }
                }
            }

            return scoreBoard;
        }

        private static int[,] BestMoveFour(int[,] board)
        {
            int[,] scoreBoard = new int[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i < board.GetLength(0) - 3)
                    {
                        if (board[j, i] == 0 && board[j, i + 1] == 0 && board[j, i + 2] == 0 && board[j, i + 3] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j, i + 1]++;
                            scoreBoard[j, i + 2]++;
                            scoreBoard[j, i + 3]++;
                        }
                    }

                    if (j < board.GetLength(1) - 3)
                    {
                        if (board[j, i] == 0 && board[j + 1, i] == 0 && board[j + 2, i] == 0 && board[j + 3, i] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j + 1, i]++;
                            scoreBoard[j + 2, i]++;
                            scoreBoard[j + 3, i]++;
                        }
                    }
                }
            }

            return scoreBoard;
        }

        private static int[,] BestMoveThree(int[,] board)
        {
            int[,] scoreBoard = new int[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i < board.GetLength(0) - 2)
                    {
                        if (board[j, i] == 0 && board[j, i + 1] == 0 && board[j, i + 2] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j, i + 1]++;
                            scoreBoard[j, i + 2]++;
                        }
                    }

                    if (j < board.GetLength(1) - 2)
                    {
                        if (board[j, i] == 0 && board[j + 1, i] == 0 && board[j + 2, i] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j + 1, i]++;
                            scoreBoard[j + 2, i]++;
                        }
                    }
                }
            }

            return scoreBoard;
        }

        private static int[,] BestMoveTwo(int[,] board)
        {
            int[,] scoreBoard = new int[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i < board.GetLength(0) - 1)
                    {
                        if (board[j, i] == 0 && board[j, i + 1] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j, i + 1]++;
                        }
                    }

                    if (j < board.GetLength(1) - 1)
                    {
                        if (board[j, i] == 0 && board[j + 1, i] == 0)
                        {
                            scoreBoard[j, i]++;
                            scoreBoard[j + 1, i]++;
                        }
                    }
                }
            }

            return scoreBoard;
        }

    }
}