//Written By Christopher Cooke
//Gem Quest Board Analyzer
//Interperets board object to identify combos, adjacent squares, & falling gems
//Loaded with recursion & double recursion
using System.Collections.Generic;

public class boardAnalyzer
{
    //Public Variables -- Laziness on the properties
    public const int yCoordIndex = 1, xCoordIndex = 0, dirCount = 4;
    public enum directionIndex { down = 0, right = 1, left = 2, up = 3 };
    public int[,] directions = { { 0, -1 }, { 1, 0 }, { -1, 0 }, { 0, 1 } };

    //Private Variables
    board board;
    int currentDirection = 0;
    List<List<boardSquare>> movesLists = new List<List<boardSquare>>();

    //Properties
    public List<List<boardSquare>> MovesList { get { return movesLists; } }
    public board Board { get { return board; } }    //To update the board or to recurse with current instance
    public int DirectionX { get { return directions[currentDirection, xCoordIndex]; } }
    public int DirectionY { get { return directions[currentDirection, yCoordIndex]; } }

    //Constructor
    public boardAnalyzer(board boardInstance, int direction)  //Overridden constructor -- boardAnalyzer.directionIndex
    {
        board = boardInstance;
        currentDirection = direction;
    }

    //Utility Methods
    public bool CheckSquaresEqual(boardSquare original, boardSquare nextSquare)
    {
        if (original.gemPrefab == nextSquare.gemPrefab && original.gemPrefab != null)
        {
            return true;
        }
        return false;
    }
    private void ClearFlags()
    {
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                //Clear all flags here
                boardSquare square = board.Squares[board.Get1DIndexFrom2D(x, y, board.Width)];
                square.Comboable = false;
                square.Destructable = false;
            }
        }
    }   

    //Adjacency Checking -- Beware Double Recursion
    public List<boardSquare> CheckSquareForAdjacency(boardSquare square)    //Overrides itself
    {
        List<boardSquare> adjacentSquares = new List<boardSquare>();
        for (int tempDir = 0; tempDir < dirCount; tempDir++)
        {
            adjacentSquares = RecurseAdjacencyCheck((int)square.transform.position.x, (int)square.transform.position.y, (int)directions[tempDir, xCoordIndex], (int)directions[tempDir, yCoordIndex], adjacentSquares, square, true);
        }
        return adjacentSquares;
    }
    public List<boardSquare> CheckSquareForAdjacency(boardSquare square, List<boardSquare> adjacentSquares)
    {
        for (int tempDir = 0; tempDir < dirCount; tempDir++)
        {
            adjacentSquares = RecurseAdjacencyCheck((int)square.transform.position.x, (int)square.transform.position.y, (int)directions[tempDir, xCoordIndex], (int)directions[tempDir, yCoordIndex], adjacentSquares, square, true);
        }
        return adjacentSquares;
    }
    //Returns list of adjacent gems marked destructable
    List<boardSquare> RecurseAdjacencyCheck(int x, int y, int dirX, int dirY, List<boardSquare> squareList, boardSquare originalSquare, bool searchStart)  //Template method
    {
        boardSquare square = board.GetBoardStruct().StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];
        if (searchStart)    //Recurse again if able
        {
            if ((x + dirX < board.Width && x + dirX >= 0)
                    && (y + dirY < board.Height && y + dirY >= 0))
            {
                return RecurseAdjacencyCheck(x + dirX, y + dirY, dirX, dirY, squareList, originalSquare, false);
            }            
        }
        else   //If not the starting object, compare and check
        {
            if (square.gemPrefab == originalSquare.gemPrefab && originalSquare.gemPrefab != null)
            {
                if (!squareList.Contains(square))
                {
                    squareList.Add(square);
                    square.Destructable = true;
                    return CheckSquareForAdjacency(square, squareList);
                }
            }
        }
        return squareList;  //Default escape
    }
   
    //Combo Checking
    public board CheckAllSquaresForCombo()  //Identify comboable squares
    {
        ClearFlags();
        int combosExisting = 0;
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                for (int tempDir = 0; tempDir < dirCount; tempDir++)
                {
                    boardStruct tempBoard = board.GetBoardStruct(); //Temp doesn't matter... Objects are reference types
                    List<boardSquare> newMove = new List<boardSquare>();
                    foreach (boardSquare bs in RecurseComboCheck(x, y, (int)directions[tempDir, xCoordIndex], (int)directions[tempDir, yCoordIndex], tempBoard.StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)], tempBoard))
                    {
                        if (!newMove.Contains(bs))
                            newMove.Add(bs);
                        foreach(boardSquare adjSq in CheckSquareForAdjacency(bs))
                        {
                            if (!newMove.Contains(adjSq))
                                newMove.Add(adjSq);
                        }
                        combosExisting++;
                        bs.Comboable = true;    
                        bs.Destructable = true;
                    }
                    if (!movesLists.Contains(newMove))
                        movesLists.Add(newMove);              
                }
            }
        }
        return board;
    }
    public List<boardSquare> RecurseComboCheck(int x, int y, int dirX, int dirY, boardSquare originalSquare, boardStruct tempBoard)
    {
        List<boardSquare> validSquares = new List<boardSquare>();
        boardSquare nextSquare = tempBoard.StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];
        int counter = 0;

        //Check next space on board
        if ((x + dirX < board.Width && x + dirX >= 0)
            && (y + dirY < board.Height && y + dirY >= 0) && !originalSquare.Comboable)
        {
            if (CheckSquaresEqual(originalSquare, nextSquare))
            {
                counter += 1;
                validSquares.Add(nextSquare);   //Squares do match
                return RecurseComboCheck(x + dirX, y + dirY, dirX, dirY, originalSquare, tempBoard, counter, validSquares);
            }            
            validSquares.Clear();
            return validSquares;   //Squares do not match
        }
        else
        {
            validSquares.Clear();   //Out of range
            return validSquares;
        }
    }
    List<boardSquare> RecurseComboCheck(int x, int y, int dirX, int dirY, boardSquare originalSquare, boardStruct tempBoard, int counter, List<boardSquare> validSquares)
    {
        boardSquare nextSquare = tempBoard.StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];
        if (counter == 3)
        {
            return validSquares;
        }
        else
        {
            //Check next space on board
            if ((x + dirX < board.Width && x + dirX >= 0)
                && (y + dirY < board.Height && y + dirY >= 0))
            {
                if (CheckSquaresEqual(originalSquare, nextSquare))
                {
                    counter += 1;
                    validSquares.Add(nextSquare);
                    return RecurseComboCheck(x + dirX, y + dirY, dirX, dirY, originalSquare, tempBoard, counter, validSquares);
                } 
                validSquares.Clear();
                return validSquares;   //Squares do not match
            }
            else
            {
                validSquares.Clear();
                return validSquares;   //Out of range
            }
        }
    }
    
    //Gem Falling   
    public boardSquare RecurseToNextGem(int x, int y, int dirX, int dirY)   //Recurse in opposite direction to square with gem
    {
        boardSquare square = board.GetBoardStruct().StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];

        if (square.Gem == null)
        {
            if ((x - dirX < board.Width && x - dirX >= 0) 
                && (y - dirY < board.Height && y - dirY >= 0))
            {
                return RecurseToNextGem(x - dirX, y - dirY, dirX, dirY);
            }
            else
            {
                if (x - dirX == board.Width)
                {
                    return board.GetBoardStruct().RightStructCoreSquare[y];
                }
                else if (x - dirX == -1)
                {
                    return board.GetBoardStruct().LeftStructCoreSquare[y];
                }
                else if (y - dirY == board.Height)
                {
                    return board.GetBoardStruct().TopStructCoreSquare[x];
                }
                else if (y - dirY == -1)
                {
                    return board.GetBoardStruct().BotStructCoreSquare[x];
                }                
                return null;
            }
        }
        else
        {
            return square;
        }
    }
} 