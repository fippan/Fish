using UnityEngine;

public class Spyglass : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject quad;

    private void Start()
    {
        OnUngrab();
    }

    public void OnGrab()
    {
        if (!cam.activeInHierarchy) cam.gameObject.SetActive(true);
        if (!quad.activeInHierarchy) quad.gameObject.SetActive(true);
    }

    public void OnUngrab()
    {
        if (cam.activeInHierarchy) cam.gameObject.SetActive(false);
        if (quad.activeInHierarchy) quad.gameObject.SetActive(false);
    }
}
