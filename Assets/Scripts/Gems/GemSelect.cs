using UnityEngine;

public class GemSelect : MonoBehaviour
{

    public float scaleIncreasePercent = .2f;
    Vector3 currentScale;
    Vector3 oldScale;

    bool editMode = false;
    bool windows = false;
    bool android = false;
    bool iPhone = false;
    bool osx = false;

    bool currentlySelected = false;

    private void OnEnable()
    {
        //subscribe the function, Exit, to the event in the PauseMenus.sc
        PauseMenus.exitGem += Exit;
    }

    private void OnDisable()
    {
        //unsubscribe the function, Exit, from the event in the PauseMenus.sc
        PauseMenus.exitGem -= Exit;
    }

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
        currentScale = this.transform.localScale;
        oldScale = currentScale;
    }

    private void Update()//this is causing an isue at the moment - Koester
    {
        //if (CheckTouching())
        //    OnMouseEnter();
        //else
        //    OnMouseExit();
    }

    private bool CheckTouching()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.transform.gameObject != null && hit.transform.gameObject == this.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void Enter(bool wasClicked)
    {
        if (ResultsScript.isGameOver)
            return;
        if (Time.timeScale > 0 && !currentlySelected) //What is this for? - CC
        {
            currentScale += (currentScale * scaleIncreasePercent);
            gameObject.transform.localScale = currentScale;
            //print(gameObject.transform.localScale);
            if (!wasClicked)
            {
                Sound.SelectGem();
            }
            else
                boardSquare.wasClicked = false;
            // transform.parent.GetComponent<BoxCollider>().enabled = true;
            currentlySelected = true;   //CC
        }
    }

    public void Exit()
    {
        if (ResultsScript.isGameOver)
            return;
        currentScale = oldScale;
        gameObject.transform.localScale = currentScale;
        currentlySelected = false;  //CC
        //print(gameObject.transform.localScale);
        //transform.parent.GetComponent<BoxCollider>().enabled = false;
    }
}
