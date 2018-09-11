using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase : MonoBehaviour
{

	public abstract void OnEnterState();
	public abstract void OnExitState();

	public virtual void OnPauseState() { }
	public virtual void OnUnPauseState() { }
}
