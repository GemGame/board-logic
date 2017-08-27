using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemExplosion : MonoBehaviour {

	public void SpawnEffect(GameObject effectPrefab, float lifeSpan, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject clone = effectPrefab;
        Destroy(Instantiate(clone, position, rotation, parent), lifeSpan);
    }
    
}
