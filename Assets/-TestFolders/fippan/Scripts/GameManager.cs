using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Menu, InGame, GameOver }

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager GetInstance
	{ get { return instance; } }

	//Variables
	private GameStates gameStatesEnum;
	private Stack<GameStateBase> gameStateStack = new Stack<GameStateBase>();

	private GameStateBase currentState;

	private void Awake()
	{
		instance = this;
		ChangeState(GameStates.Menu);
	}

	private void ChangeState(GameStates newState)
	{
		//run OnExitState method in current state
		currentState.OnExitState();

		//Push new gamestate to the stack, corresponding to the GameState enum
		switch (gameStatesEnum)
		{
			case GameStates.Menu:
				gameStateStack.Push(new MenuState());
				break;
			case GameStates.InGame:
				break;
			case GameStates.GameOver:
				gameStateStack.Push(new GameOverState());
				break;
			default:
				break;
		}


		//Set Current state to the top element of the stack and pop it
		currentState = gameStateStack.Pop();

		//run OnEnterState method in new state
		currentState.OnEnterState();

	}

	public GameStateBase GetCurrentState()
	{
		return currentState;
	}

	public void TriggerGameOver()
	{
		ChangeState(GameStates.GameOver);

		//TODO: Save score to high score list/file?  

	}
}
