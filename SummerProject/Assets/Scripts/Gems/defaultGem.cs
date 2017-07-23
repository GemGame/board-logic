//Written By Christopher Cooke
//Gem Quest Default Gem
//The default gem inheriting from base gem
//No special functionality built in. 
//Methods declared just to make class functional.

[System.Serializable]
public class defaultGem : baseGem {

	public override void PreDestroy()
    {
        //Debug.Log("PreDestroy()");
    }

    public override void PostDestroy()
    {
        //Debug.Log("PostDestroy");
    }
}
