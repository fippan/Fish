using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class GrabFish : MonoBehaviour {

    public GameObject coinSpray;
    public Mesh[] coin;
    public ParticleSystem ps;
    public ParticleSystem.ShapeModule sh;
    // UnityEvent start;
    // VRTK_InteractableListener listener;

    /*
    public void Event()
    {
        start = new UnityEvent();
        listener.SendMessageUpwards("InteractableObjectGrabbed");
        start.AddListener(PickupFish);
    }
    */
    public void PickupFish()
    {
        var coins = Instantiate(coinSpray, transform);
        Instantiate(ps, transform);
        coins.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Mesh;
        coins.GetComponent<ParticleSystemRenderer>().SetMeshes(coin);
    }
}
