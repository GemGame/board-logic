//Written By Christopher Cooke
//Gem Quest Board Square
//Contains all the data about an individual square, including gem data
//Contains trigger events, clear, destroy gem, & upgrade gem
using UnityEngine;

public class boardSquare : MonoBehaviour
{
    //Public Variables
    public bool isStaticSquare = false;
    public GameObject gemPrefab;
    public int gemX, gemY;

    //Private
    [SerializeField, HideInInspector]
    GameObject gem;
    [SerializeField, HideInInspector]
    baseGem gemScript;
    bool comboable = false;
    bool destructable = false;
    bool animPlaying = false;
    bool occupied = false;
    //bool empty = false;

    //Properties
   // public bool Empty { get { if (gem == null) return true; else return false; } }
    public baseGem GemScript { get { return gemScript; } }
    public GameObject Gem   //Assigns gemScript from GO
    {
        get
        {
            return gem;
        }
        set
        {
            gem = value;  
            if(gem != null)
            {
                gemScript = gem.GetComponent<baseGem>();
                gemScript.GemGO = gem;
            }
            if (gemScript == null)
            {
                Debug.Log("Gem script failed to assign");
            }
            else
            {
            }
        }
    }
    public bool Comboable { get { return comboable; }set { comboable = value; } }
    public bool Destructable { get { if (animPlaying) return false; return destructable; } set { destructable = value; } }
    public bool AnimPlaying { get { return animPlaying; } set{ animPlaying = value; } } 
    public bool Occupied { get { return occupied; } set { occupied = value; } }

    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("On trigger stay");
        if (other.gameObject.tag == "Gems")
            occupied = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("On trigger enter");

        if (other.gameObject.tag == "Gems")
        {
            occupied = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("On trigger exit");

        if (other.gameObject.tag == "Gems")
        {
           occupied = false;
        }
    }
    private void Update()
    {
        if(gemScript != null)
        {
            gemX = (int)gemScript.transform.position.x;
            gemY = (int)gemScript.transform.position.y;
        }        
    }
    public void Clear()
    {
        gemScript = null;
        //gem = null;
        gemPrefab = null;
        comboable = false;
        destructable = false;
        animPlaying = false;
        occupied = false;
    }
    public void UpgradeGem()
    {
        if (comboable && gemScript.UpgradedPrefab != null)
        {
            Debug.Log(this.transform.position + " upgraded gem");
            gemScript.DestroyGem();
            Gem = gemScript.SpawnGemCopy(this.transform, gemScript.UpgradedPrefab);
            gemScript.SetGemProperties(this.transform.position, gem);
        }
    }
    public void DestroyGem()
    {
        if(gemScript != null)
        {
            gemScript.DestroyGem();
        }
    }
    

   
}
