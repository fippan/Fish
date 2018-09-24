using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
	private Stack<State_Base> gameStateStack = new Stack<State_Base>();
	//private Scene currentScene;

	
	public void Initialize()
	{
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		Debug.Log(sceneIndex);


		switch (sceneIndex)
		{
			case 0:
				ChangeState(State_Intro.Instance);

				break;
			case 1:
				ChangeState(State_MainMenu.Instance);

				break;
			case 2:
				ChangeState(State_InGame.Instance);

				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Remove current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void ChangeState(State_Base state)
	{
		Debug.Log("Changing state");

		//Remove and clean up current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnExitState();
			gameStateStack.Pop();
		}

		//Add and initialize new state
		gameStateStack.Push(state);
		Debug.Log("State Stack count: " + gameStateStack.Count);
		gameStateStack.Peek().OnEnterState();
		Debug.Log(gameStateStack.Peek().name);
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
