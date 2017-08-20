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

    //Properties
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
            if (gem != null)
            {
                gemScript = gem.GetComponent<baseGem>();
                gemScript.GemGO = gem;
            }
            if (gemScript == null)
            {
                Debug.Log("Gem script failed to assign");
            }
        }
    }
    public bool Comboable { get { return comboable; } set { comboable = value; } }
    public bool Destructable { get { if (animPlaying) return false; return destructable; } set { destructable = value; } }
    public bool AnimPlaying { get { return animPlaying; } set { animPlaying = value; } }
    public bool Occupied { get { return occupied; } set { occupied = value; } }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Gems")
            occupied = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gems")
        {
            occupied = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Gems")
        {
            occupied = false;
        }
    }

    private void Update()
    {
        if (gemScript != null)
        {
            gemX = (int)gemScript.transform.position.x;
            gemY = (int)gemScript.transform.position.y;
        }
    }
    public void Clear()
    {
        gemScript = null;
        gemPrefab = null;
        comboable = false;
        destructable = false;
        animPlaying = false;
        occupied = false;
    }
    public void UpgradeGem()
    {
        if (gemScript.UpgradedPrefab != null)
        {
            StartCoroutine(gemScript.DestroyGem(true));
            //if (gemScript.basePrefab == null) Debug.Log("Base Prefab Null");
            if (gemScript.UpgradedPrefab == null) Debug.Log("Upgraded Prefab Null");
            Gem = gemScript.SpawnGemCopy(this.transform, gemScript.UpgradedPrefab, gemScript.basePrefab);
            if(Gem)
            gemScript.SetGemProperties(this.transform.position, gem);
        }
    }
    public void DestroyGem(bool isCombo)
    {
        if (gemScript != null)
        {
            StartCoroutine(gemScript.DestroyGem(isCombo));
        }
    }
}
