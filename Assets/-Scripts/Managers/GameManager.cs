using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameStates { State_Intro, State_MainMenu, State_InGame, State_PauseMenu, State_GameOver }

public class GameManager : GenericSingleton<GameManager>
{
	private Stack<IState_Base> gameStateStack = new Stack<IState_Base>();

	public void Initialize()
	{
		//TODO: Check if this works or make it work
		ChangeState((GameStates)SceneManager.GetActiveScene().buildIndex);


		//int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		//Debug.Log(sceneIndex);

		//switch (sceneIndex)
		//{
		//	case 0:
		//		ChangeState(GameStates.State_Intro);

		//		break;
		//	case 1:
		//		ChangeState(GameStates.State_MainMenu);

		//		break;
		//	case 2:
		//		ChangeState(GameStates.State_InGame);

		//		break;
		//	default:
		//		break;
		//}
	}

	/// <summary>
	/// Remove current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void ChangeState(GameStates state)
	{
		Debug.Log("Changing state");

		//Remove and clean up current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnExitState();
			gameStateStack.Pop();
		}

		//Add and initialize new state
		gameStateStack.Push(GetStateObject(state));
		Debug.Log("State Stack count: " + gameStateStack.Count);
		gameStateStack.Peek().OnEnterState();
	}

	/// <summary>
	/// Pause current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void PushState(IState_Base state)
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

	/// <summary>
	/// Helper function that converts state enum to state object
	/// </summary>
	/// <param name="state"></param>
	/// <returns></returns>
	private IState_Base GetStateObject(GameStates state)
	{
		switch (state)
		{
			case GameStates.State_Intro:
				return State_Intro.Instance;
			case GameStates.State_MainMenu:
				return State_MainMenu.Instance;
			case GameStates.State_InGame:
				return State_InGame.Instance;
			case GameStates.State_PauseMenu:
				return State_PauseMenu.Instance;
			case GameStates.State_GameOver:
				return State_GameOver.Instance;
			default:
				Debug.LogError("Fatal Error");
				return null;
		}
	}
}
