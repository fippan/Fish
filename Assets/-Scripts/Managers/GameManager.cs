using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
	//Singleton instance accessor
	//public static GameManager Instance { get; private set; }

	private Stack<State_Base> gameStateStack = new Stack<State_Base>();
	//private Scene currentScene;

	private void Awake()
	{
		//Instance = this;
	}

	/// <summary>
	/// Remove current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void ChangeState(State_Base state)
	{
		//Remove and clean up current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnExitState();
			gameStateStack.Pop();
		}

		//Add and initialize new state
		gameStateStack.Push(state);
		gameStateStack.Peek().OnEnterState();
	}

	/// <summary>
	/// Pause current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void PushState(State_Base state)
	{
		//Pause current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnPauseState();
		}

		//Add and initialize new state
		gameStateStack.Push(state);
		gameStateStack.Peek().OnEnterState();
	}

	/// <summary>
	/// Remove current state and resume previous state
	/// </summary>
	public void PopState()
	{
		//Remove and clean up current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnExitState();
			gameStateStack.Pop();
		}

		//Resume previous state
		if (gameStateStack.Count > 0)
			gameStateStack.Peek().OnResumeState();
		else
			Debug.Log("The state stack is empty, no previous state to resume");
	}




	public void TriggerGameOver()
	{
		ChangeState(State_GameOver.Instance);

		//TODO: Save score to high score list/file?  

	}
}
