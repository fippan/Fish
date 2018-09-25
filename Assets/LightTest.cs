using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTest : MonoBehaviour
{
    public Light light;
    public Color black;
    public Color white;

	void Update ()
    {
        light.color = Color.Lerp(white, black, Time.time * .02f);
	}
}
