using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcadeManager : MonoBehaviour {

    gemPool gemPool;
    gameManager gameManager;

    public string gemsDirectory = "Some Bad Directory";
    public int boardWidth = 8;
    public int boardHeight = 8;

    private bool updatedOnce = false;

    private void Awake()
    {
        TryCreateRequiredObjects();
        if (InitializeGemPool())
            InitializeGameBoard();
    }
    private void Start()
    {
        
    }
    //Custom Methods
    void InitializeGameBoard() //Creates the game board
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<gameManager>();
        gameManager.CreateBMInstance();
        gameManager.BoardManager.CreateBoard(boardWidth, boardHeight, gemPool);
    }
    bool InitializeGemPool()    //Fills gem array from resource directory
    {
        gemPool = GameObject.FindGameObjectWithTag("Gem Pool").GetComponent<gemPool>();
        gemPool.LoadGemsAtPath(gemsDirectory);
        if (gemPool.gameObject == null)
        {
            Debug.Log("Cannot find game object with the tag 'Gem Pool'");
            return false;
        }
        else if (gemPool.Gems.Count == 0)
        {
            ClearWizardObjects();
            return false;
        }
        else
        {
            //EditorPrefs.SetString("Gem Path", gemsDirectory);
            return true;
        }
    }
    void ClearWizardObjects()   //Deletes all objects created
    {
        //Find created objects
        GameObject gemPool = GameObject.FindGameObjectWithTag("Gem Pool");
        GameObject squares = GameObject.Find("Squares");
        GameObject board = GameObject.Find("Board");
        GameObject GM = GameObject.Find("Game Manager");

        if (GM != null) //Destroy board manager then game manager
        {
            foreach (boardManager bm in GM.GetComponents<boardManager>())
            {
                DestroyImmediate(bm);    //Down with the bad manners
            }
            DestroyImmediate(GM);
        }

        if (gemPool != null)
            DestroyImmediate(gemPool);

        if (squares != null)    //Destroy squares then board
        {
            Transform[] squareChildren = squares.GetComponentsInChildren<Transform>();
            foreach (Transform t in squareChildren)
            {
                if (t.GetInstanceID() != squares.GetInstanceID() && t != null)
                    DestroyImmediate(t.gameObject);
            }

            if (board != null)
                DestroyImmediate(board);
        }
    }
    void TryCreateRequiredObjects() //Ensure game has appropriate objects
    {
        //Create gem pool & game manager if scene is missing one
        if (!GameObject.FindGameObjectWithTag("Gem Pool"))
        {
            GameObject gemPool = new GameObject("Gem Pool");
            gemPool.tag = "Gem Pool";
            gemPool.AddComponent<gemPool>();
        }
        if (!GameObject.FindGameObjectWithTag("Game Manager"))
        {
            GameObject gameManager = new GameObject("Game Manager");
            gameManager.tag = "Game Manager";
            gameManager.AddComponent<gameManager>();
        }
    }
}
