namespace TicTacToe;

public class Game
{
    //The board is a 3x3 grid, but we just use an array of 9 entries
    private readonly string[] _board = new string[9];
    //Player & AI character starts off as empty
    private string _player = "";
    private string _ai = "";
    //Declaring whos turn it is, player goes first
    private bool _turn;
    private string _winner = string.Empty;
    //This is just for displaying
    private readonly List<int> _winningMoves = new();
    
    //Just declaring some pretty console colors
    private readonly string _normal = Console.IsOutputRedirected ? "" : "\x1b[39m";
    private readonly string _green = Console.IsOutputRedirected ? "" : "\x1b[92m";

    public void Run()
    {
        //Initialize all values to spaces initially
        for (var i = 0; i < _board.Length; i++) _board[i] = " ";
        Console.WriteLine("Welcome to Tic-Tac-Toe! Select X or O: ");
        
        //Wait until player has entered their choice of X or O
        while (_player == string.Empty)
        {
            var choice = Console.ReadLine()?.ToUpper();
            
            //Only accept X or O :)
            if (choice != "X" && choice != "O")
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            
            //Get rid of pesky spaces that could've been added
            _player = choice.Trim();
            //Make AI opposite of player
            _ai = choice == "X" ? "O" : "X";
        }
        
        //Run the main loop
        Console.Clear();
        GameLoop();
    }

    private void GameLoop()
    {
        //Main loop
        while (true)
        {
            //Means we've declared a winner
            if (_winner != string.Empty)
            {
                //End the game and print the winner
                WinSequence();
                break;
            }
            
            //Whos turn is it again?
            switch (_turn)
            {
                //Player is false
                case false:
                    PlayerMove();
                    break;
                //Ai is true
                case true:
                    AiMove();
                    break;
            }
            
            //Check all combinations
            CheckWinCondition();
        }
    }

    private void DrawGrid()
    {
        //Draw the grid as a set of lines :)
        Console.WriteLine("- - - - - - -");
        Console.WriteLine($"| {_board[0]} | {_board[1]} | {_board[2]} |");
        Console.WriteLine($"| {_board[3]} | {_board[4]} | {_board[5]} |");
        Console.WriteLine($"| {_board[6]} | {_board[7]} | {_board[8]} |");
        Console.WriteLine("- - - - - - - ");
    }

    private void CheckWinCondition()
    {
        CheckHorizontal();
        CheckVertical();
        CheckDiagonal();
        //If there's no spaces left and there's no winner, we declare a draw
        if (!_board.Contains(" ") && _winner == string.Empty) _winner = "Draw";
    }

    private void WinSequence()
    {
        //If it's a draw we don't do anything
        if (_winner == "Draw")
        {
            Console.WriteLine("Draw");
            DrawGrid();
            return;
        }
        //Write out the winner and show the grid one last time
        Console.WriteLine($"{_winner} has won the game!");
        foreach (var i in _winningMoves)
        {
            //Make the winning moves green to show where they won
            _board[i] = _green + _board[i] + _normal;
        }
        DrawGrid();
    }

    private void CheckHorizontal()
    {
        /*
         * We're iterating over every line in -> this direction ->
         * |x|x|x| 0 - 2 
         * |x|x|x| 3 - 5 
         * |x|x|x| 6 - 8
         * and if all symbols on the line are the same we declare a winner
         */
        for (var i = 0; i < _board.Length; i += 3)
        {
            CheckLineForWinner(i, i + 1, i + 2);
            if (_winner != string.Empty) return; // Stop checking if a winner is found
        }
    }

    private void CheckVertical()
    {
        //Same thing as horizontal except we're looking at every column now
        for (var i = 0; i < 3; i++)
        {
            CheckLineForWinner(i, i + 3, i + 6);
            if (_winner != string.Empty) return; // Stop checking if a winner is found
        }
    }

    private void CheckDiagonal()
    {
        // Diagonal from top-left to bottom-right
        CheckLineForWinner(0, 4, 8);
        if (_winner != string.Empty) return;
        // Diagonal from top-right to bottom-left
        CheckLineForWinner(2, 4, 6);
    }
    
    //This function checks for a winning position at 3 points for player and AI
    private void CheckLineForWinner(int p1, int p2, int p3)
    {
        // Check for player win
        if (_board[p1] == _player && _board[p2] == _player && _board[p3] == _player)
        {
            _winningMoves.AddRange([p1, p2, p3]);
            _winner = "Player";
            return;
        }

        // Check for AI win
        if (_board[p1] == _ai && _board[p2] == _ai && _board[p3] == _ai)
        {
            _winningMoves.AddRange([p1, p2, p3]);
            _winner = "CPU";
        }
    }

    private void PlayerMove()
    {
        //ask for a number representing one of the tiles, invalidate all inputs that aren't numbers between 1-9
        Console.WriteLine("Select 1-9 for your turn: ");
        DrawGrid();
        while (true)
        {
            var choice = int.TryParse(Console.ReadLine(), out var i);
            //Correct input? Is it 9 or less? Does the space already have something?
            if (choice == false || i > 9 || _board[i - 1] != " ")
            {
                Console.WriteLine("Invalid move. Try again.");
                continue;
            }

            _board[i - 1] = _player;
            Console.Clear();
            //Give AI a turn
            _turn = true;
            break;
        }
    }

    private void AiMove()
    {
        //Just choose a random non-occupied tile between 0 and the board size
        var rand = new Random();
        while (true)
        {
            var choice = rand.Next(0, _board.Length);
            if (_board[choice] == _player || _board[choice] == _ai) continue;
            _board[choice] = _ai;
            break;
        }
        _turn = false;
    }
}