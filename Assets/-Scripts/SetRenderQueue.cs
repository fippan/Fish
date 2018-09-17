using UnityEngine;
using LPWAsset;

public class SetRenderQueue : MonoBehaviour
{
	[SerializeField]
	private int renderQ = 3000;

	protected void Awake()
	{
		LowPolyWaterScript lowPolyWater = GetComponent<LowPolyWaterScript>();
		Renderer renderer = GetComponent<Renderer>();

		if (lowPolyWater)
		{
			Debug.Log("LPW");
			GetComponent<LowPolyWaterScript>().material.renderQueue = renderQ;
		}
		else if (renderer)
		{
			Debug.Log("renderer");
			Material[] materials = GetComponent<Renderer>().materials;

			for (int i = 0; i < materials.Length; ++i)
				materials[i].renderQueue = renderQ;
		}
	}
}
