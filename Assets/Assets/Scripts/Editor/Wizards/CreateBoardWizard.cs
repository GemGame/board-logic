//Written By Christopher Cooke
//Gem Quest Board Creation Wizard
//Declare your dimensions and this wizard will create you a serialized board
//Board will be created from random prefabs from selected resource folder
//Initializes most necessary game objects
using UnityEngine;
using UnityEditor;

public class CreateBoardWizard : ScriptableWizard {

    //Wizard Fields
    public string gemsDirectory = "Gems/All Gems";
    public int boardWidth = 8;
    public int boardHeight = 8;

    gemPool gemPool;
    gameManager gameManager;

    //Wizard Menu Bar Items    
    [MenuItem("Editor Tools/Create Board Wizard", false)]   
    static void CreateWizard()
    {
        //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create", "Apply");
        //If you don't want to use the secondary button simply leave it out:

        ScriptableWizard.DisplayWizard<CreateBoardWizard>("Create Board", "Create", "Clear Board");
        
        
        
    }
    
    //Wizard Built In Methods
    void OnWizardCreate()   //On create button
    {
        TryCreateRequiredObjects();
        InitializeGemPool();
        InitializeGameBoard();
    }    
    void OnWizardOtherButton()  //On clear board button
    {
        ClearWizardObjects();
    }
    void OnWizardUpdate()   //Sets the help string
    {
        helpString = "Configure the board you would like to build!";
    }

    //Custom Methods
    void InitializeGameBoard() //Creates the game board
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<gameManager>();
        gameManager.CreateBMInstance();
        gameManager.BoardManager.CreateBoard(boardWidth, boardHeight, gemPool);        
    }
    void InitializeGemPool()    //Fills gem array from resource directory
    {
        gemPool = GameObject.FindGameObjectWithTag("Gem Pool").GetComponent<gemPool>();
        gemPool.LoadGemsAtPath(gemsDirectory);
        if (gemPool.gameObject == null)
        {
            Debug.Log("Cannot find game object with the tag 'Gem Pool'");
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

