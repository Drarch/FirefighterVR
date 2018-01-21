using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMouseCameraInput : MonoBehaviour {

    private Vector3 mousePosStart;
    public float speed = 0.75f;

    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Jump"))
        {
            mousePosStart = Input.mousePosition;
        }

		if(Input.GetButton("Jump"))
        {
            Vector3 rotation = new Vector3(mousePosStart.y - Input.mousePosition.y, Input.mousePosition.x - mousePosStart.x, 0) * speed;

            transform.Rotate(rotation);

            mousePosStart = Input.mousePosition;
        }
	}
}
