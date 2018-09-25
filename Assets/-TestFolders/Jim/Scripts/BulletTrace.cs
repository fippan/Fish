using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] float lifetime = .1f;

    public void BulletLine(Vector3 start, Vector3 end)
    {
            LineRenderer newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            Vector3[] points = new Vector3[2];
            points[0] = start;
            points[1] = end;
            newLine.SetPositions(points);
            Destroy(newLine.gameObject, lifetime);
    }
}
