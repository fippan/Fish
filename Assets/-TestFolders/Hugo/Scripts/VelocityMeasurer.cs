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

    private bool gettingVelocity;
    private List<float> velocityMagnitudes;

    void Start()
    {
        velocityEst = GetComponent<VRTK_VelocityEstimator>();
    }

    void Update()
    {
        //Debug.Log("Veclocity: " + velocityEst.GetVelocityEstimate().magnitude);
        if (holdingRod && fishingRod.thrown != true && fishingRod.throwable)
        {
            currentSpeed = velocityEst.GetVelocityEstimate();
            if (currentSpeed.x > throwSpeed.x || currentSpeed.z > throwSpeed.z || currentSpeed.x < -throwSpeed.x || currentSpeed.z < -throwSpeed.z)
            {
                if (currentSpeed.x < throwSpeed.x / 2 || currentSpeed.z < throwSpeed.z / 2 || currentSpeed.x > -throwSpeed.x / 2 || currentSpeed.z > -throwSpeed.z / 2)
                {
                    //Debug.Log(currentSpeed);
                    fishingRod.ThrowBob(currentSpeed);
                }
            }
        }
    }

    public void StartThrow()
    {
        velocityMagnitudes = new List<float>();
        gettingVelocity = true;
        StartCoroutine(GetVelocity());
    }

    private IEnumerator GetVelocity()
    {
        while (gettingVelocity)
        {
            velocityMagnitudes.Add(velocityEst.GetVelocityEstimate().magnitude);
            yield return null;
        }
    }

    public void EndThrow()
    {
        gettingVelocity = false;
        fishingRod.OnThrowBob(velocityMagnitudes);
    }

    public void HoldingRod()
    {
        holdingRod = !holdingRod;
    }
}
