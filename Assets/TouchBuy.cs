using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TouchBuy : MonoBehaviour
{


	
    void TouchingObject ()
    {
        GameObject touching = GetComponent<VRTK_InteractTouch>().GetTouchedObject();
        if(touching.tag == "Weapon")
        {
            GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = touching.GetComponent<VRTK_InteractableObject>();
        }
    }
}
