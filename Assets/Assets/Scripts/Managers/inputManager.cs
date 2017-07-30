//Written By Christopher Cooke
//Gem Quest Input Manager
//Basic controls that allow the user to interact with the board (add android!)
//Needs animation manager hooked up to ensure no animations are playing to take input -- Consider moving this requirment elsewhere, seems lame 

using UnityEngine;
using System.Collections;



public class inputManager : MonoBehaviour {

    //Private Variables
    animationManager animationManager;

    //Properties
    public animationManager AnimationManager { set { animationManager = value; } }

    //Methods
    private void Awake()
    {

    }

    public boardSquare GetInput()
    {
#if UNITY_EDITOR
        return GetSquareOnClick();
#elif UNITY_IPHONE
          Debug.Log("Iphone");
        

#elif UNITY_STANDALONE_OSX
          Debug.Log("Stand Alone OSX");

#elif UNITY_STANDALONE_WIN
           return GetSquareOnClick();

#elif UNITY_ANDROID
        return GetSquareOnTap();
#endif
    }
    boardSquare GetSquareOnTap()
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
    boardSquare GetSquareOnClick()
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
