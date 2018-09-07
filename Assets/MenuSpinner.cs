using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuSpinner : MonoBehaviour
{
    VRTK_ControllerEvents events;

	void Start ()
    {
        GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
	}

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        PickButton(e.touchpadAxis, e.touchpadAngle);
    }

    private void PickButton (Vector2 touchpadAxis, float touchpadAngle)
    {
        float normalAngle = touchpadAngle - 90;
        if (normalAngle < 0)
            normalAngle = 360 + normalAngle;

        Debug.Log("Axis " + touchpadAxis + " Angle:  " + normalAngle);
    }
}
