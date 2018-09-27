using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform VRTK_Root;

    public void SetDestination(Vector3 position)
    {
        VRTK_Root.position = Vector3.LerpUnclamped(VRTK_Root.position, position, .1f);
    }
}
