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

    [SerializeField]
    CountDownScript countDownScript;

    //Properties
    public boardManager BoardManager { get { return boardManager; } }

    //Methods
    private void Start()
    {
        countDownScript = GameObject.Find("CountDownText").GetComponent<CountDownScript>();
        ConfigureComponents();
    }
    private void ConfigureComponents()
    {
        boardManager = this.gameObject.GetComponent<boardManager>();
        inputManager = this.gameObject.AddComponent<inputManager>();
        animationManager = this.gameObject.AddComponent<animationManager>();
        animationManager.GemFallingSpeed = boardManager.board.gemFallingSpeed;
        inputManager.AnimationManager = animationManager;
    }
    private void Update()   //Main game loop
    {
        UpdateBoard();
        moveMade = AcceptInput();
    }
    void UpdateBoard()
    {

        boardManager.UpdateComboableSquares();
        if (boardManager.Board.DetectComboableSquares())
            boardManager.DestroyComboableSquares();
        animationManager.PlayFallingAnimations(boardManager.GetFallingGemsList());
        if (moveMade)
        {
            StartCoroutine(UpdateScore());
            //subtract turn
            countDownScript.SetTurns(-1);
        }

    }
    bool AcceptInput()
    {
        if (!animationManager.CheckAnimationsPlaying())
        {
            boardSquare square = inputManager.GetInput();
            if (square != null)
            {
                //ending the game
                if (countDownScript.Turns == 0 || countDownScript.CountDowTimer == 0)
                {
                    ManageScore ms = GameObject.Find("Canvas").GetComponent<ManageScore>();
                    ms.results.SetActive(true);//enabling the gameobject, reults, which ultimately ends the game
                    return false;
                }
                square.DestroyGem(false);
                return true;
            }
        }
        return false;
    }
    IEnumerator UpdateScore()
    {
        while (animationManager.CheckAnimationsPlaying() && boardManager.board.HasEmptySquares)
        {
            yield return null;
        }
        yield return null;
        if (boardManager.TempScore > 0)
        {
            //add seconds if the score that is earned is high enough
            if (boardManager.TempScore > 500)
            {
                countDownScript.AddTime((int)(boardManager.TempScore / 100));
            }
            GameObject.Find("Score").GetComponent<AddingScore>().AddScore(boardManager.TempScore);
            boardManager.TempScore = 0;
        }

    }
    public void CreateBMInstance()  //Board Manager is needed during editor mode when creating new boards
    {
        boardManager = this.gameObject.AddComponent<boardManager>();
    }
}
