using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform VRTK_Root;
    [SerializeField] private Transform cameraEye;

    public void SetDestination()
    {
        Vector3 pos = new Vector3(-0.13f, 0f, 0f);
        VRTK_Root.position = pos;
        VRTK_Root.rotation = Quaternion.Euler(new Vector3(0f, cameraEye.rotation.y + 180f, 0f));
    }

    public void SetDestination(Vector3 position)
    {
        VRTK_Root.position = Vector3.LerpUnclamped(VRTK_Root.position, position, .1f);
    }
}
