Console.WriteLine("Welcome to Battleship!");
// there is one ship of length 5, one of length 4, two of length 3, and one of length 2
Console.WriteLine("There is one ship of length 5, one of length 4, two of length 3, and one of length 2.");

// initialize 10x10 grid
char[,] grid = new char[10, 10];

int misses = 20; // number of misses the player has to guess the ships
int hits = 0; // number of hits the player has made
Console.WriteLine($"You have {misses} misses to guess the ships. Good luck!");

// each grid starts with a dot. If it is a hit, it will be an 'X', if it is a miss, it will be a ' '
// initilize the grid with dots
for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        grid[i, j] = '.';
    }
}

Console.WriteLine("Here is the initial grid:");
printBoard(grid);

// initialize ships
char[,] shipKey = setBoard(); // setBoard also prints the ship locations

while (misses > 0)
{
    Console.WriteLine($"You have {misses} misses left.");
    grid = guessSpot(grid, shipKey);
}

Console.WriteLine("Game over! You've run out of misses.");
Environment.Exit(0); // exit the game if the player runs out of misses


// print grid method
void printBoard(char[,] gameBoard)
{
    Console.WriteLine(new string('-', 41));
    for (int i = 0; i < gameBoard.GetLength(0); i++)
    {
        Console.Write("| ");
        for (int j = 0; j < gameBoard.GetLength(1); j++)
        {
            Console.Write(gameBoard[i, j] + " | ");
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', 41));
    }
}

// guess a coordinate and handle the input
char[,] guessSpot(char[,] gameBoard, char[,] ships)
{
    Console.WriteLine("Enter where you want to place an X (row and column):");
    string input = Console.ReadLine();
    string[] coordinates = input.Split(' ');

    int row = int.Parse(coordinates[0]);
    int col = int.Parse(coordinates[1]);

    if (ships[row, col] == 'X')
    {
        Console.WriteLine("Hit!");
        gameBoard[row, col] = 'X'; // mark the hit on the game board
        hits++;
        if (hits == 17)
        {
            Console.WriteLine("Congratulations! You've sunk all the ships!");
            Environment.Exit(0); // exit the game if all ships are sunk
        }
    }
    else
    {
        Console.WriteLine("Miss!");
        misses--;
        gameBoard[row, col] = ' '; // mark the miss on the game board
    }

    printBoard(grid);
    return gameBoard;
}

// set the board with ships

char[,] setBoard()
{
    // make a second array to hold ships and compare the gameboard to it?
    char[,] ships = new char[10, 10];


    // place ships of differnet Lengths
    setShip(ships, 5); // place ship of length 5
    setShip(ships, 4); // place ship of length 4
    setShip(ships, 3); // place first ship of length 3
    setShip(ships, 3); // place second ship of length 3
    setShip(ships, 2); // place ship of length 2

    // print board with ships
    Console.WriteLine("Here is the array of placed ships:");
    printBoard(ships);

    return ships;
}

// method to help set ships on the board
void setShip(char[,] gameBoard, int shipLength)
{

    // todo: check if a position is already occupied by another ship


    // this method will be used to place ships of different lengths
    Random random = new Random();
    bool isHorizontal = random.Next(2) == 0; // 50% chance for horizontal or vertical
    int startRow, startCol;

    if (isHorizontal)
    {
        startRow = random.Next(10); // row can be anywhere from 0 to 9
        startCol = random.Next(10 - shipLength + 1); // column must allow for the full length of the ship
    }
    else
    {
        startRow = random.Next(10 - shipLength + 1); // row must allow for the full length of the ship
        startCol = random.Next(10); // column can be anywhere from 0 to 9
    }

    // place the ship on the game board
    for (int i = 0; i < shipLength; i++)
    {
        if (isHorizontal)
        {
            gameBoard[startRow, startCol + i] = 'X'; // place ship horizontally
        }
        else
        {
            gameBoard[startRow + i, startCol] = 'X'; // place ship vertically
        }
    }
}