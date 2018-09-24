using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class State_Intro : State_Base
{
	//Static instance accessor
	public static State_Intro Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		Debug.Log("Instance created of " + typeof(State_Intro).ToString());
	}


	public override void OnEnterState()
	{
		//Load new scene if not already loaded
		if (SceneManager.GetActiveScene().name != "1_Intro")
			SceneManager.LoadScene("1_Intro");

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
