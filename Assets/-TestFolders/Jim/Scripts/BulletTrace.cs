using System.Collections;
using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] float lifetime = .1f;
    [SerializeField] bool useObjectPooling;

    private void Start()
    {
        if (useObjectPooling)
            ObjectPooler.Instance.AddPool("BulletTrace", linePrefab.gameObject, 5);
    }

    public void NewTrace(Vector3 start, Vector3 end)
    {
        LineRenderer newLine;

        if (useObjectPooling)
            newLine = ObjectPooler.Instance.GetPooledObject("BulletTrace").GetComponent<LineRenderer>();
        else
            newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);

        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;
        newLine.SetPositions(points);

        if (useObjectPooling)
        {
            newLine.gameObject.SetActive(true);
            StartCoroutine(DeactivateLine(newLine.gameObject));
        }
        else
        {
            Destroy(newLine.gameObject, lifetime);
        }
    }

    private IEnumerator DeactivateLine(GameObject line)
    {
        yield return new WaitForSeconds(lifetime);
        line.SetActive(false);
    }
}
