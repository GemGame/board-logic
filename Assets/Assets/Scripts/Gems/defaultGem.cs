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

    public override void PreDestroy()
    {
        //Debug.Log("PreDestroy()");
        //StartCoroutine(WaitTime());
        if (Application.isPlaying)  //Not in editor mode
        {
            GameObject clone = explosionPrefab;
            Destroy(Instantiate(clone, transform.position, transform.rotation), 1);
            floatingTextValue.text = "+" + value.ToString();
            clone = floatingTextPrefab;
            Instantiate(clone, transform.position, transform.rotation);
            Destroy(gameObject);

        }
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


            ;

        //Debug.Log("PostDestroy()");
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
