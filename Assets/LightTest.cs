using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTest : MonoBehaviour
{
    public Light myLight;
    [Space(20)]
    public Color dark;
    public Color bright;

    public void Update()
    {
        myLight.color = Color.Lerp(bright, dark, (Time.time * .05f));
    }
}
