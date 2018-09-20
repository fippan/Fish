using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LPWAsset;

[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{
	[Header("Buoyancy")]
	[SerializeField] private Transform[] m_BuoyancyPoints;
	[SerializeField] private float m_BuoyancyStrength = 2f;
	[SerializeField] private float m_BuoyancyDamp = 2f;
	[SerializeField] private float m_FloatSpeedDamp = 0.75f;

	[Header("Fake bobbing")]
	[SerializeField] private bool m_FakeBobbing = true;
	[SerializeField] private float m_BobbingSpeed = 0.7f;
	[SerializeField] private float m_BobbingHeight = 0.05f;

	private Rigidbody m_ThisRigidbody;
	private bool m_InWater = false;
	private bool m_InBoat = false;
	private float m_WaterLevel;

	private float m_LastSine;
	private float m_Sine;

	private float m_RandomBobbingOffset;

	private void Start()
	{
		m_ThisRigidbody = GetComponent<Rigidbody>();

		m_RandomBobbingOffset = Random.Range(-0.05f, 0.05f);

		if (m_BuoyancyPoints.Length == 0)
		{
			m_BuoyancyPoints = new Transform[1];
			m_BuoyancyPoints[0] = transform;
		}
	}

	private void FixedUpdate()
	{
		if (m_InWater && !m_InBoat)
		{
			foreach (Transform item in m_BuoyancyPoints)
			{
				float forceFactor = 1f - (item.position.y - m_WaterLevel) * m_BuoyancyStrength;

				if (forceFactor >= 0.0f)
				{
					Vector3 uplift = -Physics.gravity * (forceFactor - m_ThisRigidbody.velocity.y) / m_BuoyancyPoints.Length;

					m_ThisRigidbody.AddForceAtPosition(uplift, item.position);
				}
			}

			if (m_FakeBobbing)
			{
				m_LastSine = m_Sine;
				m_Sine = Mathf.Sin(Time.fixedTime * Mathf.PI * (m_BobbingSpeed + m_RandomBobbingOffset)) * m_BobbingHeight;

				float yPosNew = transform.position.y + m_Sine - m_LastSine;

				transform.position = new Vector3(transform.position.x, yPosNew, transform.position.z);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("InsideBoat"))
		{
			m_InBoat = true;
		}

		if (other.CompareTag("Water") && !m_InBoat)
		{
			m_WaterLevel = other.transform.position.y;

			m_ThisRigidbody.angularDrag = m_BuoyancyDamp;
			m_ThisRigidbody.drag = m_FloatSpeedDamp;

			m_InWater = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("InsideBoat"))
		{
			m_InBoat = false;
		}

		if (other.CompareTag("Water"))
		{
			m_ThisRigidbody.angularDrag = 0.05f;
			m_ThisRigidbody.drag = 0f;

			m_InWater = false;
		}
	}
}
