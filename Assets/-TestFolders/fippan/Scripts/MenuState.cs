using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameStateBase
{
	//Static instance accessor
	public static MenuState Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}


	public override void OnEnterState()
	{
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
