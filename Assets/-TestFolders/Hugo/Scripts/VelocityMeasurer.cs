using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VelocityMeasurer : MonoBehaviour
{
    public FishingRod fishingRod;
    VRTK_VelocityEstimator velocityEst;
    
    private Vector3 throwSpeed = new Vector3(2, 2, 2);

    private bool gettingVelocity;
    private List<float> velocityMagnitudes;

    void Start()
    {
        velocityEst = GetComponent<VRTK_VelocityEstimator>();
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
}
