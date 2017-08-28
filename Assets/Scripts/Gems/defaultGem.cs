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
        PlayDestroyEffects();        
    }

    public override void PostDestroy()
    {
        try
        {
            Sound.sound.PlayOneShot(explosionSound, PauseMenus.SFXvolume);
        }
        catch
        {

        }  
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
        //mySprite.Play("ChargeUp", 0, 0);
        yield return null;
        //yield return new WaitForSeconds(1f);
        GameObject clone = explosionPrefab;
        Destroy(Instantiate(clone, transform.position, transform.rotation), 1);
        floatingTextValue.text = value.ToString();
        clone = floatingTextPrefab;
        Destroy(Instantiate(clone, transform.position, transform.rotation), 4);
        Destroy (gameObject);
    }
}
