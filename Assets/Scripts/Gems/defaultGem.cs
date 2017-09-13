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
    int originalValue;
    private void Start()
    {
        originalValue = value;
    }

    public override void PreDestroy()
    {
        arcadeManager am = null;
        if (GameObject.Find("ArcadeManager"))
            am = GameObject.Find("ArcadeManager").GetComponent<arcadeManager>();
        if (Application.isPlaying && ((am != null && !am.wakingUp) || (am == null)))  //Not in editor mode
        {
            PauseMenus.ExitMyGem();
            mySprite.enabled = true;
            mySprite.Play("ChargeUp", 0, 0);
            StartCoroutine(WaitTime());
        }
        else
            Destroy(gameObject);

    }

    public override void PostDestroy()
    {
        PlayDestroyEffects(value);
    }

    void PlayDestroyEffects(int addScore)
    {
        arcadeManager am = null;
        if (GameObject.Find("ArcadeManager"))
            am = GameObject.Find("ArcadeManager").GetComponent<arcadeManager>();
        if (Application.isPlaying && ((am != null && !am.wakingUp)||(am == null)))  //Not in editor mode
        {
            if (addScore > 0)
                AddingScore.addingScore.UpdateScore(addScore);
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Transform explosionParent = GameObject.FindGameObjectWithTag("Gem Pool").transform;
            Transform floatingTextParent = null;//this.gameObject.transform;
            gemExplosion ge = gameObject.AddComponent<gemExplosion>();

            ge.SpawnEffect(explosionPrefab, 1.0f, position, rotation, explosionParent);
            floatingTextValue.text = "+" + value.ToString();
            ge.SpawnEffect(floatingTextPrefab, 2.3f, position, rotation, floatingTextParent);
            try
            {
                if (runningCor == 0)
                    Sound.sound.PlayOneShot(explosionSound, PauseMenus.SFXvolume);
            }
            catch
            {

            }
            value = originalValue;
            Destroy(gameObject);
        }
    }
    public override void UpgradeGem()
    {
        //StopAllCoroutines();
        //mySprite.enabled = true;
        //mySprite.Play("ChargeUp2", 0, 0);
    }

    private IEnumerator WaitTime()
    {
        //adding to the amout of curotines that are running. We will need to keep track of this number
        runningCor++;
        yield return new WaitForSeconds(1.5f);
        //once courotine is done, we will subtract
        runningCor--;//assigning the boolean, canSelect, to the value of runningCor, if running cor is 0 canSelect will be set to true
        if (runningCor <= 0)
        {
            runningCor = 0;
            gameManager.canSelect = true;//setting this to false in boardSquare.sc UpgradeGem();
        }
       // gameManager.canSelect = (runningCor == 0);
        //in the function DestroyComboableSquares()
        PlayDestroyEffects(0);
    }
}
