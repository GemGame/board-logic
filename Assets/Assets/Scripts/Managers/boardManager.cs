﻿//Written By Christopher Cooke
//Gem Quest Board Manager
//Creates board & utilizes of board analyzer to make decisions on & about the board
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class boardManager : MonoBehaviour
{

    //Public Variables   
    [SerializeField]
    public gemUpgrade[] gemUpgrades;// = new gemUpgrade[0];
    public board board;
    public enum directionIndex { down = 0, right = 1, left = 2, up = 3 };

    //Private Variables
    [SerializeField, HideInInspector]
    int currentDirection = 0;
    int outerOffset = 1;
    int numOuterRows = 1;
    GameObject boardGO;
    List<List<boardSquare>> moveList = new List<List<boardSquare>>();

    //Properties
    public int CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
    public board Board { get { /*Debug.Log(board);*/ return board; } }//boardGO.GetComponent<board>(); } }

    private void Start()
    {
        SetUpDefaultUpgrades();
    }
    void SetUpDefaultUpgrades()
    {
        if(gemUpgrades.Length == 0)
        {
            gemUpgrades = new gemUpgrade[7];
            gemUpgrades[0] = new gemUpgrade(3, 1);
            gemUpgrades[1] = new gemUpgrade(5, 2);
            gemUpgrades[2] = new gemUpgrade(7, 3);
            gemUpgrades[3] = new gemUpgrade(9, 4);
            gemUpgrades[4] = new gemUpgrade(10, 5);
            gemUpgrades[5] = new gemUpgrade(11, 6);
            gemUpgrades[6] = new gemUpgrade(12, 7);
        }
    }
    //Methods
    public void CreateBoard(int boardWidth, int boardHeight, gemPool gemPool)
    {
        boardGO = new GameObject("Board");
        board = boardGO.AddComponent<board>();
        //Debug.Log(gemPool);
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
        moveList = new List<List<boardSquare>>();
        moveList = bA.MovesList;
        //Debug.Log("Finished Updating Targetable Squares");
    }
    public bool TryDestroyGem(boardSquare square)
    {
        if (square != null && square.Gem != null && square.Destructable && !square.AnimPlaying)
        {
            square.GemScript.DestroyGem();
            square.Clear();
            //Debug.Log("Destroyed a square");
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
                        if (!square.Gem.activeSelf)
                            square.Gem.SetActive(true);
                    }
                }
            }
        }
        return fallingSquares;
    }
    List<boardSquare> RemoveListDuplicates(List<boardSquare> move)
    {
        List<boardSquare> distinctMove = new List<boardSquare>();
        distinctMove = move.Distinct().ToList();

        return distinctMove;
    }
    public void OptimizeMoveList()
    {
        //Clean up individual elements
        if (moveList.Count > 0)
        {
            moveList = SortMultiList(moveList);
            for (int x = 0; x < moveList.Count; x++)
            {
                moveList[x] = RemoveListDuplicates(moveList[x]);
            }
            moveList = RemoveListsDuplicates(moveList);
        }


    }
    bool ListConstainsList(List<boardSquare> origList, List<boardSquare> listToCompare)
    {
        bool origListContainsAllNewList = true;
        foreach(boardSquare bs in listToCompare)
        {
            if (!listToCompare.Contains(bs))
                origListContainsAllNewList = false;
        }
        return origListContainsAllNewList;
    }
    List<List<boardSquare>> RemoveListsDuplicates(List<List<boardSquare>> moveList)
    {
        int length = moveList.Count;
        int potentialNewIndex = 0;
        //List<boardSquare> tempMove;
        for (int moveIndex = 0; moveIndex < length; moveIndex++)    //For each move
        {
            potentialNewIndex = moveIndex;
            for (int newMove = moveIndex + 1; newMove < length; newMove++)    //For eah remaining move
            {
                //bool containsAll = false;
                if (ListConstainsList(moveList[potentialNewIndex], moveList[newMove]))    //Compare items
                {
                    if (moveList[potentialNewIndex].Count >= moveList[newMove].Count)
                    {
                        moveList.Remove(moveList[newMove]);
                        newMove--;
                        length = moveList.Count;
                    }
                }
            }
            // tempMove = moveList[moveIndex];   //Store copy of bs
            //moveList[moveIndex] = moveList[potentialNewIndex];  //Try update move
            //moveList[potentialNewIndex] = tempMove;
        }
        return moveList;
    }
    List<List<boardSquare>> SortMultiList(List<List<boardSquare>> moveList)
    {
        int length = moveList.Count;
        int potentialNewIndex = 0;
        List<boardSquare> tempMove;
        for (int moveIndex = 0; moveIndex < length; moveIndex++)    //For each move
        {
            potentialNewIndex = moveIndex;
            for (int newMove = moveIndex + 1; newMove < length; newMove++)    //For eah remaining move
            {
                if (moveList[potentialNewIndex].Count <= moveList[newMove].Count)    //Compare items
                {
                    potentialNewIndex = newMove;
                }
            }
            tempMove = moveList[moveIndex];   //Store copy of bs
            moveList[moveIndex] = moveList[potentialNewIndex];  //Try update move
            moveList[potentialNewIndex] = tempMove;
        }
        return moveList;
    }
    List<boardSquare> SortSingleList(List<boardSquare> move)
    {
        int length = move.Count;
        int potentialNewIndex = 0;
        boardSquare tempSquare;
        for (int moveIndex = 0; moveIndex < length; moveIndex++)    //For each move
        {
            potentialNewIndex = moveIndex;
            for (int newMove = moveIndex; newMove < length; newMove++)    //For eah remaining move
            {
                if (move[potentialNewIndex].gemX > move[newMove].gemX && move[potentialNewIndex].gemY < move[newMove].gemY)    //Compare items
                {
                    potentialNewIndex = newMove;
                }
            }
            tempSquare = move[moveIndex];   //Store copy of bs
            move[moveIndex] = move[potentialNewIndex];  //Try update move
            move[potentialNewIndex] = tempSquare;
        }
        return move;
    }
    public bool ContainsAllItems(List<boardSquare> a, List<boardSquare> b)
    {
        return !b.Except(a).Any();
    }
    public void DestroyComboableSquares()
    {
        OptimizeMoveList();
        /*
        Debug.Log(moveList.Count);
        foreach(boardSquare bs in moveList[0])
        {
            Debug.Log(bs);
        }
        */
        foreach (List<boardSquare> move in moveList)
        {
            bool listMoving = false;
            foreach (boardSquare bs in move)
            {
                if (bs.AnimPlaying)
                {
                    listMoving = true;
                }
            }
            if (!listMoving)
            {
                upgradeController uc = new upgradeController(gemUpgrades);
                List<boardSquare> movesToRemove = uc.GetRandomUpgradedGemList(move);
                foreach (boardSquare bs in movesToRemove)
                {
                     //Debug.Log("Attempting to upgrade " + bs);
                    bs.UpgradeGem();
                    move.Remove(bs);
                }
                foreach (boardSquare bs in move)
                {
                    //Debug.Log("Attempting to destroy " + bs);
                    TryDestroyGem(bs);
                }
            }
        }
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
                        square.Gem = square.gemPrefab.GetComponent<baseGem>().SpawnGemCopy(square.transform, square.gemPrefab, square.gemPrefab);
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
