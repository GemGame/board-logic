//Written By Christopher Cooke
//Gem Quest Input Manager
//Basic controls that allow the user to interact with the board (add android!)
//Needs animation manager hooked up to ensure no animations are playing to take input -- Consider moving this requirment elsewhere, seems lame 
using UnityEngine;

public class inputManager : MonoBehaviour {

    //Private Variables
    animationManager animationManager;

    //Properties
    public animationManager AnimationManager { set { animationManager = value; } }

    //Methods
    public boardSquare GetSquareOnClick()
    {
        if (Input.GetMouseButtonDown(0) && !animationManager.CheckAnimationsPlaying())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject != null)
                    return hit.transform.gameObject.GetComponent<boardSquare>();
            }
        }
        return null;
    }
}
