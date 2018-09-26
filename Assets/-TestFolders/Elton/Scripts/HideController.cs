using UnityEngine;

public class HideController : MonoBehaviour
{
    private bool didHideDefaultController;

	void LateUpdate ()
    {
        HideDefaultControllerIfNeeded();
	}

    void HideDefaultControllerIfNeeded()
    {
        if (!didHideDefaultController)
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.name == "Standard (Instance)")
                {
                    renderers[i].enabled = false;
                    didHideDefaultController = true;
                }
            }
        }
    }
}
