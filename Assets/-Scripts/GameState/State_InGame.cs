using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class State_InGame : State_Base
{
	//Static instance accessor
	public static State_InGame Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}


	public override void OnEnterState()
	{
		//Load new scene if not already loaded
		if (SceneManager.GetActiveScene().name != "3_InGame")
			SceneManager.LoadScene("3_InGame");


		throw new System.NotImplementedException();
	}

	public override void OnExitState()
	{
		throw new System.NotImplementedException();

	}

	public override void OnPauseState()
	{
		throw new System.NotImplementedException();

	}

	public override void OnResumeState()
	{
		throw new System.NotImplementedException();

	}
}
