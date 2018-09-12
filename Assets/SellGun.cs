using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SellGun : VRTK_InteractableObject
{
    public GameObject gun;
    public float cost;
    public bool hasBeenBought = false;

    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        if (hasBeenBought == false)
        {
            base.StartUsing(currentUsingObject);
            VRTK_InteractGrab myGrab = currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>();
            myGrab.AttemptGrab();
        }
    }
}
