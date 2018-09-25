using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] float lifetime = .1f;

    private void Start()
    {
        ObjectPooler.Instance.AddPool("BulletTrace", linePrefab.gameObject, 5);
    }

    public void BulletLine(Vector3 start, Vector3 end)
    {
        LineRenderer newLine = ObjectPooler.Instance.GetPooledObject("BulletTrace").GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;
        newLine.SetPositions(points);
        newLine.gameObject.SetActive(true);
        Destroy(newLine.gameObject, lifetime);
    }
}
