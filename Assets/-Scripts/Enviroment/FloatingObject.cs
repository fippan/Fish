using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LPWAsset;

public class FloatingObject : MonoBehaviour
{
	[SerializeField] private Transform[] m_BuoyancyPoints;
	[SerializeField] private float m_BuoyancyStrength = 1f;
	[SerializeField] private float m_BobbingDamp = 0.5f;

	private Rigidbody m_ThisRigidbody;
	private bool m_InWater = false;
	private float m_WaterLevel;


	private void Start()
	{
		m_ThisRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (m_InWater)
		{
			foreach (Transform item in m_BuoyancyPoints)
			{
				float forceFactor = 1f - (item.position.y - m_WaterLevel) * m_BuoyancyStrength;

				if (forceFactor > 0f)
				{
					Vector3 uplift = -Physics.gravity * (forceFactor - m_ThisRigidbody.velocity.y) / m_BuoyancyPoints.Length;
					m_ThisRigidbody.AddForceAtPosition(uplift, item.position);
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			Vector3 waterPosition = other.transform.position;
			//float waterHeightExtent = other.GetComponent<MeshRenderer>().bounds.extents.y;
			//m_WaterLevel = waterPosition.y + waterHeightExtent;

			m_WaterLevel = waterPosition.y;

			m_ThisRigidbody.angularDrag = m_BobbingDamp;

			m_InWater = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			m_ThisRigidbody.angularDrag = 0.05f;

			m_InWater = false;
		}
	}

}
