using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class State_Intro : GenericSingleton<State_Intro>, IState_Base
{
	public void OnEnterState()
	{
		AsyncOperation loadOperation = null;

		//Load new scene if not already loaded
		if (SceneManager.GetActiveScene().name != GlobalVariables.introScene)
			loadOperation = SceneManager.LoadSceneAsync(GlobalVariables.introScene, LoadSceneMode.Single);

		StartCoroutine(ExecuteAfterLevelLoad(loadOperation));
	}

	private IEnumerator ExecuteAfterLevelLoad(AsyncOperation op)
	{
		//Wait for scene to be completely loaded
		if (op != null)
		{
			yield return new WaitUntil(() => op.isDone);
			Debug.Log("Level has been loaded: " + SceneManager.GetActiveScene().name);
		}

		Debug.Log("Level has been loaded: " + SceneManager.GetActiveScene().name);
		//Code to execute afte rlevel has been loaded


	}

	public void OnExitState()
	{
		Debug.Log("Exiting state");


	}

	public void OnPauseState()
	{
		throw new System.NotImplementedException();

	}

	public void OnResumeState()
	{
		throw new System.NotImplementedException();

	}
}

