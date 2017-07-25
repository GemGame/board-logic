//Written By Christopher Cooke
//Gem Quest Board Manager
//Creates board & utilizes of board analyzer to make decisions on & about the board
using UnityEngine;
using System.Collections.Generic;

public class boardManager : MonoBehaviour {

    //Public Variables   
    public board board;    
    public enum directionIndex { down = 0, right = 1, left = 2, up = 3 };

    //Private Variables
    int currentDirection = 0;
    int outerOffset = 1;
    int numOuterRows = 1;
    GameObject boardGO;
    List<List<boardSquare>> moveList = new List<List<boardSquare>>();

    //Properties
    public int CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
    public board Board { get { /*Debug.Log(board);*/ return board; } }//boardGO.GetComponent<board>(); } }

    //Methods
    public void CreateBoard(int boardWidth, int boardHeight, gemPool gemPool)
    {
        boardGO = new GameObject("Board");
        board = boardGO.AddComponent<board>();
        Debug.Log(gemPool);
        board.SetBoardProperties(boardWidth, boardHeight, outerOffset, numOuterRows, gemPool);
        board.InitializeDefaultBoard();
        board.InitializeOuterRows();
        UpdateComboableSquares();
        DestroyComboableSquares(true);
    }
    public void UpdateComboableSquares()
    {
        boardAnalyzer bA = new boardAnalyzer(board, currentDirection);
        board = bA.CheckAllSquaresForCombo();
        moveList = bA.MovesList;
        //Debug.Log("Finished Updating Targetable Squares");
    }
    public bool TryDestroyGem(boardSquare square)
    {
        if (square != null && square.Gem != null && square.Destructable && !square.AnimPlaying)
        {
            square.GemScript.DestroyGem();
            square.Clear();
            Debug.Log("Destroyed a square");
        }
        return square.Destructable;
    }
    boardSquare GetNextGemToFall(boardSquare square)
    {
        boardAnalyzer bA = new boardAnalyzer(board, currentDirection);
        return bA.RecurseToNextGem((int)square.transform.position.x, (int)square.transform.position.y, bA.DirectionX, bA.DirectionY);
    }
    public void DestroyAdjacentSquares(boardSquare square)
    {
        if (square.Comboable)
        {
            boardAnalyzer bA = new boardAnalyzer(board, currentDirection);
            foreach (boardSquare bs in bA.CheckSquareForAdjacency(square))
            {
                if (bs != square)
                {
                    TryDestroyGem(bs);
                    Debug.Log(bs);
                }
            }
        }
    }
    public List<boardSquare> GetFallingGemsList()
    {
        List<boardSquare> fallingSquares = new List<boardSquare>();

        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                // int count = 0;
                boardSquare square = board.Squares[board.Get1DIndexFrom2D(x, y, board.Width)];
                // Debug.Log("Analyzing fall for square : " + square);
                if (square.Gem == null)//&& count == 0)
                {
                    //   count++;
                    //Debug.Log("Square is empty");
                    if (board.TrySwapSquareGems(square, GetNextGemToFall(square)))
                    {
                        if (!fallingSquares.Contains(square))
                            fallingSquares.Add(square);
                    }
                }
            }
        }
        return fallingSquares;
    }    
    public void DestroyComboableSquares()
    {
        foreach(List<boardSquare> move in moveList)
        {
            bool listMoving = false;
            foreach(boardSquare bs in move)
            {
                if (bs.AnimPlaying)
                {
                    listMoving = true;
                }
            }
            if (!listMoving)
            {
                foreach(boardSquare bs in move)
                {
                    TryDestroyGem(bs);
                }
            }
        }
        /*
        boardSquare[] Board = board.GetBoardStruct().StructCoreSquare;
        foreach(boardSquare bs in Board)
        {
            if(bs.Comboable)
            {
                TryDestroyGem(bs);
            }
        }
        foreach(boardSquare bs in Board)
        {
            DestroyAdjacentSquares(bs);
        }
        */
    }
    public void DestroyComboableSquares(bool onCreate)  //Prevent wizard from creating a board with combos
    {
        while (board.DetectComboableSquares())// && count < 8000)
        {
            foreach (boardSquare square in board.GetBoardStruct().StructCoreSquare)
            {
                if (square.Comboable && square.Gem != null)
                {
                    TryDestroyGem(square);
                    if (onCreate)   //Works
                    {
                        square.gemPrefab = board.GemPool.GetRandomGem(square.transform);
                        square.Gem = square.gemPrefab.GetComponent<baseGem>().SpawnGemCopy(square.transform, square.gemPrefab);
                        square.Gem.name = "Gem[" + square.gemX + ", " + square.gemY + "]";
                    }
                    else
                    {
                        square.gemPrefab = null;
                        Debug.Log(square + " destroyed and left empty");
                    }
                }
                if (board.DetectComboableSquares())
                    UpdateComboableSquares();
            }
            if (board.DetectComboableSquares())
                UpdateComboableSquares();
        }
    }
   
    
} 
