//Written By Christopher Cooke
//Gem Quest Input Manager
//Mouse & Touchscreen controls that allow the user to interact with the board 
using UnityEngine;

public class inputManager : MonoBehaviour {

    //Private Variables
    animationManager animationManager;
    bool editMode = false;
    bool windows = false;
    bool android = false;
    bool iPhone = false;
    bool osx = false;

    //Properties
    public animationManager AnimationManager { set { animationManager = value; } }

    //Methods
    private void Awake()    //Preprocess Platform Determination
    {
        #if UNITY_EDITOR
            editMode = true;
        #elif UNITY_IPHONE
            iPhone = true;
        #elif UNITY_STANDALONE_OSX
            osx = true;
        #elif UNITY_STANDALONE_WIN
            windows = true;
        #elif UNITY_ANDROID
            android = true;
        #endif
    }

    //Class Return Method
    public boardSquare GetInput()
    {
        if (android)
            return GetSquareOnTap();
        else
            return GetSquareOnClick();
    }

    boardSquare GetSquareOnTap()    //Android
    {
        foreach(Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject != null)
                {
                    boardSquare square = hit.transform.gameObject.GetComponent<boardSquare>();
                    if (square != null && !square.isStaticSquare)
                    {
                        return square;
                    }
                }
            }
        }
        return null;
    }
    boardSquare GetSquareOnClick()  //Win / Editor
    {
        if (Input.GetMouseButtonDown(0) && !animationManager.CheckAnimationsPlaying())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject != null)
                {
                    boardSquare square = hit.transform.gameObject.GetComponent<boardSquare>();
                    if(square != null && !square.isStaticSquare)
                    {
                        return square;
                    }
                }
            }
        }
        return null;
    }
}
