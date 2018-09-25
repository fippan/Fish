using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_MainMenu : GenericSingleton<State_MainMenu>, IState_Base
{
	public void OnEnterState()
	{
		if (SceneManager.GetActiveScene().name != "2_MainMenu")
			SceneManager.LoadScene("2_MainMenu");


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
