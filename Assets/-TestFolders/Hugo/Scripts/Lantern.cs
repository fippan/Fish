using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject light;
    private bool lightToggle = false;

    public void ToggleLight ()
    {
        lightToggle = !lightToggle;
        light.SetActive(lightToggle);
    }
}
