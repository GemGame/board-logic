//Written By Christopher Cooke
//Gem Quest Board Class
//Holds arrays of boardSquares and contains methods to initialize & spawn everything
//A board is a pretty public thing
using UnityEngine;

public struct boardStruct   //Retrieves all board square arrays
{
    //Intended to be designed out to hold all relevant data about a board without having to pass methods as well
    boardSquare[] structCoreSquare;    
    public boardSquare[] StructCoreSquare
    {
        get { return structCoreSquare; }
        set { structCoreSquare = value; }
    }
    boardSquare[] topStructCoreSquare;
    public boardSquare[] TopStructCoreSquare
    {
        get { return topStructCoreSquare; }
        set { topStructCoreSquare = value; }
    }
    boardSquare[] leftStructCoreSquare;
    public boardSquare[] LeftStructCoreSquare
    {
        get { return leftStructCoreSquare; }
        set { leftStructCoreSquare = value; }
    }
    boardSquare[] botStructCoreSquare;
    public boardSquare[] BotStructCoreSquare
    {
        get { return botStructCoreSquare; }
        set { botStructCoreSquare = value; }
    }
    boardSquare[] rightStructCoreSquare;
    public boardSquare[] RightStructCoreSquare
    {
        get { return rightStructCoreSquare; }
        set { rightStructCoreSquare = value; }
    }
}
public class board : MonoBehaviour  {

    //Public Variables
    public float gemFallingSpeed = 3.25f;
    //Private Variables - Loads of serialization due to being created in editor
    [SerializeField, HideInInspector]
    int width = 8;
    [SerializeField, HideInInspector]
    int height = 8;
    [SerializeField, HideInInspector]
    int offset = 3;
    [SerializeField, HideInInspector]
    int rowCount = 3;
    [SerializeField]
    gemPool gems;
    [SerializeField]
    boardSquare[] coreSquares;
    [SerializeField]
    boardSquare[] topSquares;
    [SerializeField]
    boardSquare[] leftSquares;
    [SerializeField]
    boardSquare[] rightSquares;
    [SerializeField]
    boardSquare[] bottomSquares;
    [SerializeField]
    GameObject allSquares, cSquaresGO, tSquaresGO, lSquaresGO, rSquaresGO, bSquaresGO;

    //Properties
    public bool HasEmptySquares //Iterates over all core squares
    {
        get
        {
            for(int x = 0; x < coreSquares.Length; x++)
            {
                if(coreSquares[x].Gem == null || coreSquares[x].AnimPlaying)
                {
                    return true;
                }

            }
            return false;
        }
    }
    public gemPool GemPool { get { return gems; } }
    public int Width { set { width = value; } get { return width; } } //R/W
    public int Height { set { height = value; } get { return height; } }    //R/W
    public int Offset { set { offset = value; } }   //Write Only
    public int RowCount { set { rowCount = value; } }   //Write Only
    public boardSquare[] Squares { get { return coreSquares; } }    //Read Only

    //Methods 
    private void Update()
    {
        RefillEmptySquares();
    }
    void RefillEmptySquares()
    {
       for(int x = 0; x < width; x++)
        {
            SpawnRandomGem(topSquares[x], x, 0);
            SpawnRandomGem(bottomSquares[x], x, 0);            
        }
       for(int y = 0; y < height; y++)
        {
            SpawnRandomGem(leftSquares[y], y, 0);
            SpawnRandomGem(rightSquares[y], y, 0);
        }
    }
    void SpawnRandomGem(boardSquare square, int x, int y)
    {
        if(square.Gem == null && !square.Occupied)
        {
            square.gemPrefab = gems.GetRandomGem(square.transform);
            square.Gem = square.gemPrefab.GetComponent<baseGem>().SpawnGemCopy(square.transform, square.gemPrefab);
            square.Gem.name = "Gem[" + x + ", " + y + "]";
            square.GemScript.SetGemProperties(new Vector3(x, y, 0), square.Gem);
            square.Occupied = true;
        }
    }
    public bool DetectComboableSquares()    //Moved to board analyzer
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                boardSquare square = coreSquares[Get1DIndexFrom2D(x, y, width)];
                if (square.Comboable)
                    return true;
            }
        }
        return false;
    }
    public bool TrySwapSquareGems(boardSquare emptySquare, boardSquare gemSquare)
    {
        Debug.Log("Attempted to swap gems");
        if (gemSquare.Gem != null)
        {
            Debug.Log("Moved " + gemSquare + "to " + emptySquare);
            emptySquare.Gem = gemSquare.Gem;
            emptySquare.gemPrefab = gemSquare.gemPrefab;
            emptySquare.Gem.transform.parent = emptySquare.transform;
            gemSquare.Gem = null;
            gemSquare.gemPrefab = null;
            return true;
        }
        return false;
    }
    public boardStruct GetBoardStruct()
    {
        boardStruct board = new boardStruct();
        board.StructCoreSquare = coreSquares;
        board.TopStructCoreSquare = topSquares;
        board.LeftStructCoreSquare = leftSquares;
        board.BotStructCoreSquare = bottomSquares;
        board.RightStructCoreSquare = rightSquares;
        return board;
    }
    public int Get1DIndexFrom2D(int x, int y, int boardWidth)   //Optimize to remove 3rd param  //2d arrays are not serializable. Converted all 2d arrays to 1d. Now we're using 2d again.
    {
        return y * boardWidth + x;
    }
    public void SetBoardProperties(int boardWidth, int boardHeight, int outerRowOffset, int outerRowCount, gemPool gemPool)
    {
        gems = gemPool;
        width = boardWidth;
        height = boardHeight;
        offset = outerRowOffset;
        rowCount = outerRowCount;
    }
    boardSquare InitializeSquare(boardSquare square, int x, int y, GameObject parent)
    {
        if(parent == null)
        {
            parent = new GameObject("Unknown Parent");
        }
        //square = new boardSquare();
        GameObject sq = new GameObject("Square[" + x + ", " + y + "]");
        sq.tag = "Square";
        sq.transform.position = new Vector3(x, y, 0);
        square = sq.AddComponent<boardSquare>();        
        BoxCollider col =  sq.AddComponent<BoxCollider>();    //Make the square clickable 
        col.isTrigger = true;
        sq.transform.parent = parent.transform;
        SpawnRandomGem(square, x, y);
        //square.GemScript.SpawnGem(sq.transform);
        
        //square.gem = square.GemScript.GemGO;
        square.gemX = x;
        square.gemY = y;
        return square;     
    }
    public void InitializeOuterRows()   //Outside core squares
    {
        //Width dependent
        topSquares = new boardSquare[width];
        bottomSquares = new boardSquare[width];
        //Height dependent
        leftSquares = new boardSquare[height];
        rightSquares = new boardSquare[height];
        //Width dependent
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < rowCount; y++)
            {
                topSquares[x] = InitializeSquare(topSquares[x], x, y + height + offset, tSquaresGO);
                bottomSquares[x] = InitializeSquare(bottomSquares[x], x, -y-1 - offset, bSquaresGO);                
            }
        }
        //Height dependent
        for (int x = 0; x < rowCount; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // X & Y reversed
                int leftIndex = y;//Get1DIndexFrom2D(x, y, width);
                leftSquares[leftIndex] = InitializeSquare(leftSquares[leftIndex], -x - 1 - offset, y, lSquaresGO);
                rightSquares[y] = InitializeSquare(rightSquares[y], x + width + offset, y, rSquaresGO);
            }
        }
    }
    public void InitializeDefaultBoard()	// Build the board randomly
    {
        coreSquares = new boardSquare[width * height];
        CreateBoardGameObjects();
        if(gems == null)
        {
            Debug.Log("Error loading gems");
        }
        else
        {
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    coreSquares[Get1DIndexFrom2D(x,y,width)] = InitializeSquare(coreSquares[Get1DIndexFrom2D(x,y,width)], x, y, cSquaresGO);                   
                }
            }            
        }
    }    
    public void CreateBoardGameObjects()
    {
        allSquares = new GameObject("Squares");
        cSquaresGO = new GameObject("Core");
        tSquaresGO = new GameObject("Top Border");
        bSquaresGO = new GameObject("Bottom Border");
        lSquaresGO = new GameObject("Left Border");
        rSquaresGO = new GameObject("Right Border");
        cSquaresGO.transform.parent = allSquares.transform;
        tSquaresGO.transform.parent = allSquares.transform;
        bSquaresGO.transform.parent = allSquares.transform;
        lSquaresGO.transform.parent = allSquares.transform;
        rSquaresGO.transform.parent = allSquares.transform;
        allSquares.transform.parent = GameObject.Find("Board").transform;
    } 
} 
