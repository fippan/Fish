using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State_MainMenu : State_Base
{
	//Static instance accessor
	public static State_MainMenu Instance { get; private set; }
	private Scene scene;



	private void Awake()
	{
		Instance = this;
	}


	public override void OnEnterState()
	{
		if (scene.name != "2_MainMenu")
			SceneManager.LoadScene("2_MainMenu");


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
