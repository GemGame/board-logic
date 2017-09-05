//Written By Christopher Cooke
//Gem Quest Default Gem
//The default gem inheriting from base gem
//No special functionality built in. 
//Methods declared just to make class functional.
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class defaultGem : baseGem {
    //just a friendly reminder, the gem's animator is off by default. You must enable the animator, if you 
    //want to animate a gem -Koester

    public override void PreDestroy()
    {
        mySprite.enabled = true;
        StartCoroutine (WaitTime());
    }

    public override void PostDestroy()
    {
        PlayDestroyEffects();
    }

    void PlayDestroyEffects()
    {
        arcadeManager am = null;
        if (GameObject.Find("ArcadeManager"))
            am = GameObject.Find("ArcadeManager").GetComponent<arcadeManager>();
        if (Application.isPlaying && ((am != null && !am.wakingUp)||(am == null)))  //Not in editor mode
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Transform explosionParent = GameObject.FindGameObjectWithTag("Gem Pool").transform;
            Transform floatingTextParent = null;//this.gameObject.transform;
            gemExplosion ge = gameObject.AddComponent<gemExplosion>();

            ge.SpawnEffect(explosionPrefab, 1.0f, position, rotation, explosionParent);
            floatingTextValue.text = "+" + value.ToString();
            ge.SpawnEffect(floatingTextPrefab, 1.0f, position, rotation, floatingTextParent);
            try
            {
                if (runningCor == 0)
                    Sound.sound.PlayOneShot(explosionSound, PauseMenus.SFXvolume);
            }
            catch
            {

            }
            Destroy(gameObject);
        }
    }
    public override void UpgradeGem()
    {
        //Debug.Log("PostDestroy()");
        //creating upgrade effect -Koester
        //GameObject clone = upgradeEffectPrefab;
        //Destroy(Instantiate(clone, transform.position, transform.rotation), 1);
    }

    private IEnumerator WaitTime()
    {
        mySprite.Play("ChargeUp", 0, 0);
        //adding to the amout of curotines that are running. We will need to keep track of this number
        runningCor++;
        yield return new WaitForSeconds(1.5f);
        //once courotine is done, we will subtract
        runningCor--;//assigning the boolean, canSelect, to the value of runningCor, if running cor is 0 canSelect will be set to true
        if (runningCor <= 0)
            runningCor = 0;
        gameManager.canSelect = (runningCor == 0); //this bool, "gameManager.canSelect gets set to true in boardManager
        //in the function DestroyComboableSquares()
        PlayDestroyEffects();
    }
}
