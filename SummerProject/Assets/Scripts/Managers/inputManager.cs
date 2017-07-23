using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour {

    animationManager animationManager;

    public animationManager AnimationManager { set { animationManager = value; } }
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
