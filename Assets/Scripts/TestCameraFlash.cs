using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TestCameraFlash : MonoBehaviour {

    public VRTK_HeadsetFade fade;

    private VRTK_ControllerReference controllerReference;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject rightHand = VRTK_DeviceFinder.GetControllerRightHand(true);
        controllerReference = VRTK_ControllerReference.GetControllerReference(rightHand);
        
        if (IsPressed() || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FlashCamera());
        }
	}

    private bool IsPressed()
    {
        if (!VRTK_ControllerReference.IsValid(controllerReference))
        {
            return false;
        }

        if(VRTK_SDK_Bridge.GetControllerButtonState(SDK_BaseController.ButtonTypes.ButtonTwo, SDK_BaseController.ButtonPressTypes.Press, controllerReference))
        {
            Debug.Log("VR flash");
            return true;
        }

        return false;
    }

    IEnumerator FlashCamera()
    {
        fade.Fade(Color.white, 0.01f);
        yield return new WaitForSeconds(2);
        fade.Unfade(2);
    }
}
