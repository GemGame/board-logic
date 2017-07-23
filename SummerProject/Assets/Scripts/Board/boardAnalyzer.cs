//Written By Christopher Cooke
//Gem Quest Board Analyzer
//Interperets board object to identify combos, adjacent squares, & falling gems
//Loaded with recursion & double recursion
using System.Collections.Generic;
using UnityEngine;

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
    public List<boardSquare> CheckSquareForAdjacency(boardSquare square)
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
            //Debug.Log("Attempting recursion on original square");
            if ((x + dirX < board.Width && x + dirX >= 0)
                    && (y + dirY < board.Height && y + dirY >= 0))
            {
                return RecurseAdjacencyCheck(x + dirX, y + dirY, dirX, dirY, squareList, originalSquare, false);
            }
            
        }
        else   //If not the starting object, compare and check
        {
            //Debug.Log(square + " is not equal to " + originalSquare);
            if (square.gemPrefab == originalSquare.gemPrefab && originalSquare.gemPrefab != null)
            {
                if (!squareList.Contains(square))
                {
                    squareList.Add(square);
                    //square.Comboable = true;
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
                        newMove.Add(bs);
                        foreach(boardSquare adjSq in CheckSquareForAdjacency(bs))
                        {
                            newMove.Add(adjSq);
                        }
                        combosExisting++;
                         //Debug.Log("Combos found so far : " + combosExisting);
                        bs.Comboable = true;    //10 million calls... why?
                        bs.Destructable = true;
                    }
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
            && (y + dirY < board.Height && y + dirY >= 0))
        {
            if (CheckSquaresEqual(originalSquare, nextSquare))
            {
                //    Debug.Log("Gem " + x + ", " + y + " matches square " + originalSquare.gemX + ", " + originalSquare.gemY + "! Looking another square!");
                counter += 1;
                // Debug.Log("Counter set to " + counter);
                //if (x == 0 && y == 0) Debug.Log("00 set to true");
                //nextSquare.Targetable = true;
                //if (x == 0 && y == 0) Debug.Log(board.coreSquares[board.Get1DIndexFrom2D(x, y, board.Width)].Targetable);
                validSquares.Add(nextSquare);
                return RecurseComboCheck(x + dirX, y + dirY, dirX, dirY, originalSquare, tempBoard, counter, validSquares);
            }
            //Debug.Log("Gems do not match. Counted " + counter + " matching gems.");
            //nextSquare.Targetable = false; 
            return validSquares;   //Squares do not match
        }
        else
        {
            //Debug.Log("Recursion ended due to leaving board at square : " + x + ", " + y);
            //nextSquare.Targetable = false;
            return validSquares;   //Out of range
        }
        //return nextSquare.Targetable;

    }
    List<boardSquare> RecurseComboCheck(int x, int y, int dirX, int dirY, boardSquare originalSquare, boardStruct tempBoard, int counter, List<boardSquare> validSquares)
    {
        boardSquare nextSquare = tempBoard.StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];
        if (counter == 3)
        {
            //Debug.Log("Counter reached 3, returning true.");
            //board = existingBoard;
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
                    //Debug.Log("Gem " + x + ", " + y + " matches square " + originalSquare.gemX + ", " + originalSquare.gemY + "! Looking another square!");
                    counter += 1;
                    //Debug.Log("Counter set to " + counter);
                    validSquares.Add(nextSquare);
                    //nextSquare.Targetable = true;
                    return RecurseComboCheck(x + dirX, y + dirY, dirX, dirY, originalSquare, tempBoard, counter, validSquares);
                }
                //Debug.Log("Gems do not match. Counted " + counter + " matching gems.");
                //nextSquare.Targetable = false; 
                validSquares.Clear();
                return validSquares;   //Squares do not match
            }
            else
            {
                //  Debug.Log("Recursion ended due to leaving board at square : " + x + ", " + y);
                //nextSquare.Targetable = false;
                validSquares.Clear();
                return validSquares;   //Out of range
            }
            //return nextSquare.Targetable;
        }
    }
    
    //Gem Falling   
    public boardSquare RecurseToNextGem(int x, int y, int dirX, int dirY)   //Recurse in opposite direction to square with gem
    {
        boardSquare square = board.GetBoardStruct().StructCoreSquare[board.Get1DIndexFrom2D(x, y, board.Width)];

        if (square.Gem == null)
        {
            //Debug.Log("Next square has no gem : " + x + ", " + y);
            if ((x - dirX < board.Width && x - dirX >= 0) 
                && (y - dirY < board.Height && y - dirY >= 0))
            {
                //Debug.Log("Next square is still on the board. Recursing again.");

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
                    //Debug.Log("Looking for new top square");
                    return board.GetBoardStruct().TopStructCoreSquare[x];//board.Get1DIndexFrom2D(x, 0, board.Width)];
                }
                else if (y - dirY == -1)
                {
                    //Debug.Log("Looking for new bot square");
                    //Debug.Log(x);
                    return board.GetBoardStruct().BotStructCoreSquare[x];//board.Get1DIndexFrom2D(x, 0, board.Width)];
                }                
                return null;
            }
        }
        else
        {
           // Debug.Log("Recursion ended at square : " + x + ", " + y);
            return square;
        }
    }
}