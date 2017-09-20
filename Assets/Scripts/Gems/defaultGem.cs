//Written By Christopher Cooke
//Gem Quest Default Gem
//The default gem inheriting from base gem
//No special functionality built in. 
//Methods declared just to make class functional.
using System;
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
    public override void HintGem()
    {
        mySprite.enabled = true; //turning the animator on befoe playing the animation
        mySprite.Play("Hint", 0, 0);
        //play sound
        //waiting before turning the animaiton off
        StartCoroutine(HintOff());

    }
    IEnumerator HintOff()
    {
        yield return new WaitForSeconds(1f);
        //truning the animator off
        mySprite.enabled = false;
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
                //if (runningCor == 0)
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
        yield return new WaitForSeconds(1.5f);
       // gameManager.canSelect = (runningCor == 0);
        PlayDestroyEffects(0);
    }
}
