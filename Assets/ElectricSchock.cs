using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ElectricSchock : MonoBehaviour {

    public VRTK_HeadsetFade fade;
    public Transform respawn;

    public VRTK_DashTeleport dashTeleport;
    
    // Use this for initialization
    void Start ()
    {
            //dashTeleport.WillDashThruObjects += new DashTeleportEventHandler(DashThru);
            //dashTeleport.DashedThruObjects += new DashTeleportEventHandler(RendererOn);

            //Debug.LogFormat("Teleport: {0}, {1}", dashTeleport, this.name);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void DashThru(object sender, DashTeleportEventArgs e)
    {
        //StartCoroutine(FlashCamera());
        Debug.LogFormat("Flash DashThru: {0}", this.name);
    }

    IEnumerator FlashCamera()
    {
        Debug.LogFormat("Fade: {0}", this.name);

        fade.Fade(Color.white, 0.01f);
        
        yield return new WaitForSeconds(2);
        fade.Unfade(2);
    }

    void Flash()
    {
        fade.Fade(Color.white, 0.01f);
        //fade.Unfade(4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!fade.IsFaded())
        {
            StartCoroutine(FlashCamera());
            //Flash();
        }
        Debug.LogFormat("Flash Trigger: {0}", this.name);
    }
}
