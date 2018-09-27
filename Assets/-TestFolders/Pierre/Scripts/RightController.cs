using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class RightController : VRTK_InteractGrab
{
    public IKControl inverseControl;
    public Transform rightController;
	// Use this for initialization
	void Start () {
        inverseControl = FindObjectOfType<IKControl>();
	}

    public override void OnControllerStartGrabInteractableObject(ObjectInteractEventArgs e)
    {
        Debug.Log("Picked up");
            if (e.target.GetComponent<Weapon>())
            {
                Debug.Log("PickedUpWeapon");
                if (e.target.GetComponent<Shotgun>())
                {
                Debug.Log("Picked up shotgun");
                if (!e.target.GetComponent<Shotgun>().primaryTaken)
                {
                    inverseControl.rightHandObj = e.target.GetComponentsInChildren<SnapPoint>()[0].transform;
                    Debug.Log("Primary not taken!");
                }

                else if (e.target.GetComponent<Shotgun>().primaryTaken)
                {
                    inverseControl.rightHandObj = e.target.GetComponentsInChildren<SnapPoint>()[1].transform;
                    e.target.transform.rotation = rightController.rotation;
                    Debug.Log("Primary taken!");
                }
                }

        }
   }

    public override void OnControllerUngrabInteractableObject(ObjectInteractEventArgs e)
    {
        inverseControl.rightHandObj = null;
    }
}
