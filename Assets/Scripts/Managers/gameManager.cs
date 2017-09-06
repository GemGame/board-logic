//Written By Christopher Cooke
//Gem Quest Game Manager
//The master manager through which all other managers are interacted with. 
//Contains the main game loop & controls the flow / updating of the game
using UnityEngine;
using System.Collections;

[System.Serializable]
public class gameManager : MonoBehaviour
{
    //Private variables
    [SerializeField, HideInInspector]
    boardManager boardManager;
    [SerializeField, HideInInspector]
    inputManager inputManager;
    [SerializeField, HideInInspector]
    animationManager animationManager;
    bool moveMade = false;
    bool gameOver = false;
    bool nextLevelUnlocked = false;
    CountDownScript countDownScript;
    sceneController sc;

    public static bool canSelect = true;

    //Properties
    public boardManager BoardManager { get { return boardManager; } }


    //Methods
    private void Start()
    {
        gameManager.canSelect = true;
        ConfigureComponents();
    }
    private void ConfigureComponents()
    {
        boardManager = this.gameObject.GetComponent<boardManager>();
        inputManager = this.gameObject.AddComponent<inputManager>();
        animationManager = this.gameObject.AddComponent<animationManager>();
        animationManager.GemFallingSpeed = boardManager.board.gemFallingSpeed;
        inputManager.AnimationManager = animationManager;
        countDownScript = GameObject.FindGameObjectWithTag("CountDownText").GetComponent<CountDownScript>();
    }
    private void Update()   //Main game loop
    {
        if (!gameOver && canSelect && Time.timeScale > 0)
        {
            UpdateBoard();
            UpdateTurnCount(AcceptInput());
        }
        else
        {
            UpdateBoard();
            if (!nextLevelUnlocked)
            {
                if (GameObject.Find("SceneManager"))
                {
                    sc = GameObject.Find("SceneManager").GetComponent<sceneController>();
                    nextLevelUnlocked = true;
                    UnlockNextLevel();
                }
            }
        }
    }

    void UnlockNextLevel()
    {
        int nextLevel = (sc.GetNextScene(sc.GetCurrentSceneIndex()) - 2);
        Debug.Log(nextLevel + " unlocked");
        
            level.SetLevelUnlocked(nextLevel, true);
            Debug.Log("New level unlocked");
        
    }
    IEnumerator DelayedSceneChange(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        GameObject.Find("SceneManager").GetComponent<sceneController>().TryLoadPreparedScene();
    }
    void UpdateBoard()
    {
        boardManager.UpdateComboableSquares();
            if (boardManager.Board.DetectComboableSquares())
                boardManager.DestroyComboableSquares();
            animationManager.PlayFallingAnimations(boardManager.GetFallingGemsList());            
    }
    void UpdateTurnCount(bool inputDetected)
    {
        if (inputDetected)
        {
            countDownScript.SetTurns(-1);
        }
    }
    public bool CheckGameOver()
    {
        //ending the game -Koester
        if (countDownScript.Turns == 0 || countDownScript.CountDowTimer == 0)
        {
            ManageScore ms = GameObject.Find("Canvas").GetComponent<ManageScore>();
            if (ms != null)
                ms.results.SetActive(true);//enabling the gameobject, reults, which ultimately ends the game -Koester
            return true;
        }
        return false;
    }
    bool AcceptInput()
    {
        if (!animationManager.CheckAnimationsPlaying())
        {
            if (canSelect)
                gameOver = CheckGameOver();
            boardSquare square = inputManager.GetInput();
            if (square != null)
            {
                //Debug.Log("Trying to boom");   
                square.DestroyGem(false);
                return true;
            }
        }
        return false;
    }
    public void CreateBMInstance()  //Board Manager is needed during editor mode when creating new boards
    {
        boardManager = this.gameObject.AddComponent<boardManager>();
    }
}
