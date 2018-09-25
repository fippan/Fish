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
    private Vector3 throwSpeed = new Vector3(2, 2, 2);

    void Start()
    {
        velocityEst = GetComponent<VRTK_VelocityEstimator>();
    }

    void Update ()
    {
        if (holdingRod && fishingRod.thrown == false && fishingRod.throwable)
        {
            currentSpeed = velocityEst.GetVelocityEstimate();
            if (currentSpeed.x > throwSpeed.x || currentSpeed.z > throwSpeed.z || currentSpeed.x < -throwSpeed.x || currentSpeed.z < -throwSpeed.z)
            {
                if (currentSpeed.x < throwSpeed.x / 2 || currentSpeed.z < throwSpeed.z / 2 || currentSpeed.x > -throwSpeed.x / 2 || currentSpeed.z > -throwSpeed.z / 2)
                {
                    fishingRod.GetComponent<AudioController>().Play("Throw", fishingRod.gameObject.transform.position);
                    fishingRod.ThrowBob(currentSpeed);
                }
            }
        }
	}

    public void HoldingRod ()
    {
        holdingRod = !holdingRod;
    }
}
