using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase : MonoBehaviour
{
	public abstract void OnEnterState();
	public abstract void OnExitState();

	public abstract void OnPauseState();
	public abstract void OnResumeState();

	private void ChangeState(GameStateBase state)
	{
		GameManager.Instance.ChangeState(state);
	}
}
