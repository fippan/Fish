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
			yield return new WaitUntil(() => op.isDone);
		else if (GameManager.Instance.FinishedSetup == false)
			yield return new WaitUntil(() => GameManager.Instance.FinishedSetup);

		Debug.Log("Level has been loaded: " + SceneManager.GetActiveScene().name);

		//CODE TO EXECUTE AFTER LEVEL LOADED HERE
		GameManager.Instance.headsetFade.Unfade(2f);


	}

	public void OnExitState()
	{
		Debug.Log("Exiting state: " + this.ToString());
		GameManager.Instance.headsetFade.Fade(new Color(0f, 0f, 0f), 0.3f);
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

