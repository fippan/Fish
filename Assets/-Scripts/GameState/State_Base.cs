using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Base : MonoBehaviour
{
	public abstract void OnEnterState();
	public abstract void OnExitState();

	public abstract void OnPauseState();
	public abstract void OnResumeState();

	private void ChangeState(State_Base state)
	{
		GameManager.Instance.ChangeState(state);
	}
}
