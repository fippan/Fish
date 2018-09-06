using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject Canvas;
    private bool toggleMenu = false;

    public void ToggleMenu()
    {
        toggleMenu = !toggleMenu;
        Canvas.SetActive(toggleMenu);
    }
}
