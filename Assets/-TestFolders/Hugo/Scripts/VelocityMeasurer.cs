using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VelocityMeasurer : MonoBehaviour
{
    public FishingRod fishingRod;
    public Vector3 currentSpeed;
    VRTK_VelocityEstimator velocityEst;

    private bool holdingRod = false;
    private Vector3 throwSpeed = new Vector3(3, 3, 3);

    void Start()
    {
        velocityEst = GetComponent<VRTK_VelocityEstimator>();
    }

    void Update ()
    {
        if (holdingRod)
        {
            currentSpeed = velocityEst.GetVelocityEstimate();
            if (currentSpeed.x > throwSpeed.x || currentSpeed.z > throwSpeed.z || currentSpeed.x < -throwSpeed.x || currentSpeed.z < -throwSpeed.z)
            {
                if (currentSpeed.x < throwSpeed.x / 2 || currentSpeed.z < throwSpeed.z / 2 || currentSpeed.x > -throwSpeed.x / 2 || currentSpeed.z > -throwSpeed.z / 2)
                {
                    //TODO: Send out the "Flöte" from the rod!
                    fishingRod.ThrowBob(currentSpeed);
                    Debug.Log("its working");
                }
            }
        }
	}

    public void HoldingRod ()
    {
        holdingRod = !holdingRod;
    }
}
