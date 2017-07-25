//Written By Christopher Cooke
//Gem Quest Game Manager
//The master manager through which all other managers are interacted with. 
//Contains the main game loop & controls the flow / updating of the game
using UnityEngine;

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

    //Properties
    public boardManager BoardManager { get { return boardManager; } }

    //Methods
    private void Start()
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
        AcceptInput();
    }
    void UpdateBoard()
    {
        boardManager.UpdateComboableSquares();
        if (boardManager.Board.DetectComboableSquares())
            boardManager.DestroyComboableSquares();
        animationManager.PlayFallingAnimations(boardManager.GetFallingGemsList());
    }
    void AcceptInput()
    {
        if (!animationManager.CheckAnimationsPlaying())
        {
            boardSquare square = inputManager.GetSquareOnClick();
            if (square != null)
            {
                square.GemScript.DestroyGem();
            }
        }
    }
    public void CreateBMInstance()  //Board Manager is needed during editor mode when creating new boards
    {
        boardManager = this.gameObject.AddComponent<boardManager>();
    }   
}
