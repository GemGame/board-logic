//Written By Christopher Cooke
//Gem Quest Board Manager
//Creates board & utilizes of board analyzer to make decisions on & about the board
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[System.Serializable]
public class boardManager : MonoBehaviour
{
    //Public Variables   
    [SerializeField]
    public gemUpgrade[] gemUpgrades;// = new gemUpgrade[0];
    [SerializeField]
    public board board;
    public enum directionIndex { down = 0, right = 1, left = 2, up = 3 };
    

    //Private Variables
    bool destroyAllRanThisFrame = false;
    [SerializeField, HideInInspector]
    int currentDirection = 0;
    [SerializeField]
    CountDownScript countDownScript;
    int outerOffset = 1;
    int numOuterRows = 1;
    int tempScore = 0;
    GameObject boardGO;
    List<List<boardSquare>> moveList = new List<List<boardSquare>>();
    RandomText randomText;

    //Properties
    //public int TempScore { get { return tempScore; } set { tempScore = value; } }
    public int CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
    public board Board { get { return board; } }

    //Methods
    private void Start()
    {
        randomText = gameObject.AddComponent<RandomText>(); //going to just set this up at the start of the game -Koester
        SetUpDefaultUpgrades();
        if (Application.isPlaying)
            countDownScript = GameObject.Find("CountDownText").GetComponent<CountDownScript>();
    }
    private void LateUpdate()
    {
        destroyAllRanThisFrame = false;
    }
    //Creation
    void SetUpDefaultUpgrades()
    {
        if (gemUpgrades != null)
        {
            if (gemUpgrades.Length == 0)
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
        else
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
    public void CreateBoard(int boardWidth, int boardHeight, gemPool gemPool)
    {
        boardGO = new GameObject("Board");
        board = boardGO.AddComponent<board>();
        board.SetBoardProperties(boardWidth, boardHeight, outerOffset, numOuterRows, gemPool);
        board.InitializeDefaultBoard();
        board.InitializeOuterRows();
        UpdateComboableSquares();
        DestroyComboableSquares(true);
    }

    //Destruction
    public bool TryDestroyGem(boardSquare square, bool isCombo)
    {
        if (square != null && square.Gem != null && square.Destructable && !square.AnimPlaying)
        {
            StartCoroutine(square.GemScript.DestroyGem(isCombo));  //Considering just calling square.DestroyGem but don't want to debug if I did this on purpose...
            square.Clear();
        }
        return square.Destructable;
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
                    TryDestroyGem(bs, true);
                }
            }
        }
    }   
    public void DestroyComboableSquares()   //Runtime
    {
        gameManager.canSelect = false;//this gets set to true in defaultGem.cs
        OptimizeMoveList();
        for(int x = 0; x < moveList.Count; x++) //For each move
        {            
            if (MoveAnimationEnded(moveList[x]))
            {
                //Get lists ready
                upgradeController uc = new upgradeController(gemUpgrades);
                List<boardSquare> movesToRemove = uc.GetRandomUpgradedGemList(moveList[x]);
                moveList[x] = ReduceList(moveList[x], movesToRemove);

                //Calc score before destroying
                tempScore += AddMoveListValue(moveList);

                UpgradeGemsFromList(movesToRemove);
                DestroyGemsFromList(moveList[x]);

                StartCoroutine(UpdateScore());
            }
        }
    }
    public void DestroyComboableSquares(bool inEditor)  //Editor -- Prevent wizard from creating a board with combos
    {
        while (board.DetectComboableSquares())
        {
            foreach (boardSquare square in board.GetBoardStruct().StructCoreSquare)
            {
                if (square.Comboable && square.Gem != null)
                {
                    TryDestroyGem(square, false);
                    if (inEditor)
                    {
                        square.gemPrefab = board.GemPool.GetRandomGem(square.transform);
                        square.Gem = Instantiate((GameObject)square.gemPrefab, square.transform.position,square.transform.rotation, square.transform);//square.gemPrefab.GetComponent<baseGem>().SpawnGemCopy(square.transform, square.gemPrefab, square.gemPrefab);
                        square.GemScript.basePrefab = board.GemPool.ReturnMatchingPrefab(square.gemPrefab);
                        square.Gem.transform.parent = square.transform;
                        square.Gem.name = "Gem[" + square.gemX + ", " + square.gemY + "]";
                        square.GemScript.SetGemProperties(Vector3.zero, square.Gem, square.transform);
                    }
                    else
                    {
                        square.gemPrefab = null;
                    }
                }

                if (board.DetectComboableSquares())
                    UpdateComboableSquares();
            }
            if (board.DetectComboableSquares())
                UpdateComboableSquares();
        }
    }
    void UpgradeGemsFromList(List<boardSquare> move)
    {
        foreach (boardSquare bs in move)
        {
            bs.UpgradeGem();
        }
    }
    void DestroyGemsFromList(List<boardSquare> move)
    {
        foreach (boardSquare bs in move)
        {
            TryDestroyGem(bs, true);
        }
    }   
    public void DestroyAllSquares() //Untested
    {
        List<List<boardSquare>> tempMoveList = new List<List<boardSquare>>();
        List<boardSquare> allSquares = new List<boardSquare>();
        foreach(boardSquare bs in board.GetBoardStruct().StructCoreSquare)
        {
            allSquares.Add(bs);    
        }
        tempMoveList.Add(allSquares);
        tempScore += AddMoveListValue(tempMoveList);
        DestroyGemsFromList(allSquares);
        StartCoroutine(UpdateScore());
    }

    //Control
    public void UpdateComboableSquares()
    {
        boardAnalyzer bA = new boardAnalyzer(board, currentDirection);
        board = bA.CheckAllSquaresForCombo();
        moveList = new List<List<boardSquare>>();
        moveList = bA.MovesList;
    }
    boardSquare GetNextGemToFall(boardSquare square)
    {
        boardAnalyzer bA = new boardAnalyzer(board, currentDirection);
        return bA.RecurseToNextGem((int)square.transform.position.x, (int)square.transform.position.y, bA.DirectionX, bA.DirectionY);
    }
    int AddMoveListValue(List<List<boardSquare>> moveList)
    {
        int totalScore = 0;
        foreach (List<boardSquare> move in moveList)
        {
            foreach (boardSquare bs in move)
            {
                totalScore += bs.GemScript.value;
            }
        }
        return totalScore;
    }
    IEnumerator UpdateScore()
    {
        while (board.HasEmptySquares)
        {
            yield return null;
        }
        yield return null;
        if (tempScore > 0)
        {
            //add seconds if the score that is earned is high enough
            if (tempScore > 500)
            {
                countDownScript.AddTime((int)(tempScore / 500));
            }
            //here we are going to randomize the text based on what score the player got, afterwards we will return a random, rewarding message for the player
            //-Koester
            int random = (Random.Range(0, 4));
            randomText.Congradulate(tempScore);
            GameObject.Find("Score").GetComponent<AddingScore>().AddScore(tempScore);
            tempScore = 0;
        }
    }
    bool MoveAnimationEnded(List<boardSquare> move)
    {
        foreach (boardSquare bs in move)
        {
            if (bs.AnimPlaying)
            {
                return false;
            }
        }
        return true;
    }

    //List Management -- I should make a list sort library for board games or at least another class
    //Note to self -- See if generics would let me do this with greater modularity
    List<boardSquare> ReduceList(List<boardSquare> mainList, List<boardSquare> itemsToRemove)
    {
        for(int x = 0; x < mainList.Count; x++)
        {
            if (itemsToRemove.Contains(mainList[x]))
            {
                mainList.RemoveAt(x);
                x--;
            }
        }
        return mainList;
    }
    public List<boardSquare> GetFallingGemsList()
    {
        List<boardSquare> fallingSquares = new List<boardSquare>();

        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                boardSquare square = board.Squares[board.Get1DIndexFrom2D(x, y, board.Width)];
                if (square.Gem == null)
                {
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
    List<boardSquare> RemoveListDuplicates(List<boardSquare> move)  //Return Distinct List
    {
        List<boardSquare> distinctMove = new List<boardSquare>();
        distinctMove = move.Distinct().ToList();

        return distinctMove;
    }
    public void OptimizeMoveList()  //Clean up individual elements
    {
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
        foreach (boardSquare bs in listToCompare)
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
}