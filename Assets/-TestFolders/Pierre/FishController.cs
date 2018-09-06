using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class FishController : VRTK_InteractGrab {

    VRTK_InteractGrab_UnityEvents events;
    public VRTK_InteractGrab lmao;
    ObjectInteractEventArgs e;
    ObjectInteractEventHandler h;
    // Use this for initialization
    void Start () {
        lmao = GetComponent<VRTK_InteractGrab>();
        lmao.GetGrabbedObject();
	}
	
    public override void OnControllerStartGrabInteractableObject(ObjectInteractEventArgs e)
    {
       if(e.target.transform.tag == "Fish")
        {
            e.target.GetComponent<GrabFish>().PickupFish();
        }
    }
}
