using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GlobalVariables
{
	public const string introScene = "1_Intro";
	public const string mainMenuScene = "2_MainMenu";
	public const string inGameScene = "3_InGame";

}

public enum GameStates { State_Intro, State_MainMenu, State_InGame, State_PauseMenu, State_GameOver }

public class GameManager : GenericSingleton<GameManager>
{
	private Stack<IState_Base> gameStateStack = new Stack<IState_Base>();

	/// <summary>
	/// Dummy class for initializing GameManager
	/// </summary>
	public void Initialize()
	{
		//Creates the GameManager first time it's called but does nothing
	}

	private void Awake()
	{
		ChangeState((GameStates)SceneManager.GetActiveScene().buildIndex);
		Debug.Log("Created Game manager");
	}

	/// <summary>
	/// Remove current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void ChangeState(GameStates state)
	{
		Debug.Log("Changing state to: " + state.ToString());

		//Remove and clean up current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnExitState();
			gameStateStack.Pop();
		}

		//Add and initialize new state
		gameStateStack.Push(GetStateObject(state));
		gameStateStack.Peek().OnEnterState();

		Debug.Log("Currently " + gameStateStack.Count + " states in the gamestate stack.");
	}

	/// <summary>
	/// Pause current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void PushState(GameStates state)
	{
		Debug.Log("Changing state to: " + state.ToString());

		//Pause current state
		if (gameStateStack.Count > 0)
		{
			gameStateStack.Peek().OnPauseState();
		}

		//Add and initialize new state
		gameStateStack.Push(GetStateObject(state));
		gameStateStack.Peek().OnEnterState();

		Debug.Log("Currently " + gameStateStack.Count + " states in the gamestate stack.");
	}

	/// <summary>
	/// Remove current state and resume previous state
	/// </summary>
	public void PopState()
	{
		Debug.Log("Removing " + gameStateStack.Peek().ToString() + " from stack and resuming previous state.");

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
			Debug.LogError("The state stack is empty, no previous state to resume");


		Debug.Log("Currently " + gameStateStack.Count + " states in the gamestate stack.");
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
