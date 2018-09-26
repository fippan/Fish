using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class State_InGame : GenericSingleton<State_InGame>, IState_Base
{
	public void OnEnterState()
	{
		//Load new scene if not already loaded
		if (SceneManager.GetActiveScene().name != "3_InGame")
			SceneManager.LoadScene("3_InGame");


		throw new System.NotImplementedException();
	}

	public void OnExitState()
	{
		throw new System.NotImplementedException();

	}

	public void OnPauseState()
	{
		throw new System.NotImplementedException();

	}

	public void OnResumeState()
	{
		throw new System.NotImplementedException();

	}
}
