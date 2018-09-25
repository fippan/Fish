using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class State_Intro : GenericSingleton<State_Intro>, IState_Base
{
	public void OnEnterState()
	{
		//Load new scene if not already loaded
		if (SceneManager.GetActiveScene().name != "1_Intro")
			SceneManager.LoadScene("1_Intro");

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

