using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Haptics : MonoBehaviour
{
    private GameObject newController;
    private float hapticStrength;
    private float hapticDuration;
    private float hapticInterval;
    private bool doHaptics = false;

    public static Haptics Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// <paramref name="whichController"/> is what controller you want the haptics to happen.
    /// <paramref name="strength"/> is the strength of the vibration, its clamped between 0 and 1.
    /// <paramref name="duration"/> is how many seconds the vibration will play.
    /// <paramref name="pulseInterval"/> puts pauses during the the play time.
    /// </summary>
    public void StartHaptics (GameObject whichController, float strength, float duration, float pulseInterval)
    {
        GameObject controller = whichController.GetComponent<VRTK_InteractableObject>().GetGrabbingObject();

        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controller), strength, duration, pulseInterval);
        //newController = whichController;
        //hapticStrength = strength;
        //hapticDuration = duration;
        //hapticInterval = pulseInterval;
        //doHaptics = true;
    }
}
