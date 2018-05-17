using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TestCameraFlash : MonoBehaviour {

    public VRTK_HeadsetFade fade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FlashCamera());
        }
	}

    IEnumerator FlashCamera()
    {
        fade.Fade(Color.white, 0.01f);
        yield return new WaitForSeconds(2);
        fade.Unfade(2);
    }
}
