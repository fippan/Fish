using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class HoldingSpinnerCheck : VRTK_InteractGrab
{
    VRTK_InteractGrab_UnityEvents events;
    public VRTK_InteractGrab spinner;

    public void Start()
    {
        spinner = GetComponent<VRTK_InteractGrab>();
        spinner.GetGrabbedObject();
    }

    public override void OnControllerStartGrabInteractableObject(ObjectInteractEventArgs e)
    {
        if (e.target.transform.tag == "Spinner")
        {
            e.target.GetComponent<Spinner>().IsGrabbed();
        }
    }

    public override void OnControllerUngrabInteractableObject(ObjectInteractEventArgs e)
    {
        if (e.target.transform.tag == "Spinner")
        {
            e.target.GetComponent<Spinner>().IsGrabbed();
        }
    }
}
