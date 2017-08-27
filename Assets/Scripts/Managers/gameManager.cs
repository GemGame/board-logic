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
    CountDownScript countDownScript;



    //Properties
    public boardManager BoardManager { get { return boardManager; } }

    //Methods
    private void Start()
    {
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
        if (!gameOver)
        {
            UpdateBoard();
            UpdateTurnCount(AcceptInput());
            gameOver = CheckGameOver();
        }
        else
        {
            UpdateBoard();
            //Display Game Over Here
        }
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
