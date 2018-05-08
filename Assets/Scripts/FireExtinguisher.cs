using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public FireType fireType;
    public FireTypeFloatDictionary extinguishRateDictionary;

    public ParticleSystem particleExhaust;
    
	void Update ()
    {
        Collider extinguishArea = GetComponent<Collider>();

        if (Input.GetKey(KeyCode.Space))
        {
            ParticleSystem.MainModule psMain = particleExhaust.main;
            particleExhaust.Emit(1);
            
            extinguishArea.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            extinguishArea.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.LogFormat("Hited {0}", other.name);
        Extinguish(other.gameObject.GetComponentInParent<Flammable>());
    }

    private void Extinguish(Flammable item)
    {
        if (item != null)
        {
            item.Temperature -= extinguishRateDictionary[item.fireType];
        }
    }
}
