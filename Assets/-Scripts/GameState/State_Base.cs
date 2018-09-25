using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState_Base
{
	void OnEnterState();
	void OnExitState();

	void OnPauseState();
	void OnResumeState();

	//private void ChangeState(State_Base state)
	//{
	//	GameManager.Instance.ChangeState(state);
	//}
}