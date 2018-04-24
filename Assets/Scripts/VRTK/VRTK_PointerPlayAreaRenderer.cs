using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRTK_PointerPlayAreaRenderer : VRTK_BezierPointerRenderer
{
    protected override Vector3 ProjectForwardBeam()
    {
        Vector3 baseProject = base.ProjectForwardBeam();

        return baseProject;

        Vector3 result = baseProject;


        //if (!ValidPlayArea())
        {
            RaycastHit MyRayHit;
            Vector3 direction = (transform.position - baseProject).normalized;
            Ray MyRay = new Ray(transform.position, direction);

            if (Physics.Raycast(MyRay, out MyRayHit))
            {

                if (MyRayHit.collider != null)
                {
                    //MyRayHit = destinationHit;
                    Vector3 MyNormal = MyRayHit.normal;
                    MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                    if (MyNormal == MyRayHit.transform.up) { Debug.Log("+Y"); }
                    if (MyNormal == -MyRayHit.transform.up) { Debug.Log("-Y"); }
                    if (MyNormal == MyRayHit.transform.forward) { Debug.Log("+Z"); result.z -= playareaCursor.playAreaCursorDimensions.y / 2.0f; }
                    if (MyNormal == -MyRayHit.transform.forward) { Debug.Log("-Z"); result.z += playareaCursor.playAreaCursorDimensions.y / 2.0f; }
                    if (MyNormal == MyRayHit.transform.right) { Debug.Log("+X"); result.x -= playareaCursor.playAreaCursorDimensions.x / 2.0f; }
                    if (MyNormal == -MyRayHit.transform.right) { Debug.Log("-X"); result.x -= playareaCursor.playAreaCursorDimensions.x / 2.0f; }
                }
            }

        }

        return result;
    }
}
