using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//Singleton instance accessor
	public static GameManager Instance { get; private set; }

	private Stack<GameStateBase> gameStateStack = new Stack<GameStateBase>();

	private void Awake()
	{
		Instance = this;
		ChangeState(MenuState.Instance);
	}

	/// <summary>
	/// Remove current state and enter new state
	/// </summary>
	/// <param name="state"></param>
	public void ChangeState(GameStateBase state)
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
	public void PushState(GameStateBase state)
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
		ChangeState(GameOverState.Instance);

		//TODO: Save score to high score list/file?  

	}
}
