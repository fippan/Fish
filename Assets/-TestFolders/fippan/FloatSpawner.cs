using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSpawner : MonoBehaviour
{
	public GameObject m_ObjectToSpawn;
	public int m_SpawnedObjects = 0;
	public int m_FramesToSkip = 1;

	private Vector3 m_ForceDir;

	private void Update()
	{

		if (Input.GetButton("Jump") && (Time.frameCount % m_FramesToSkip == 0))
		{
			GameObject spawnedObject = Instantiate(m_ObjectToSpawn, transform.position, Quaternion.identity, transform);
			//spawnedObject.transform.localScale = spawnedObject.transform.localScale * Random.Range(1, 7);
			

			m_ForceDir = new Vector3(Random.Range(-3, 3), 10, Random.Range(-4, 2));
			spawnedObject.GetComponent<Rigidbody>().AddForce(m_ForceDir, ForceMode.Impulse);

			m_SpawnedObjects++;
		}
	}
}
