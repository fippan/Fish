using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
	private void Start()
	{
		Invoke("LoadScene", 3f);
	}

	void LoadScene()
	{
		GameManager.Instance.ChangeState(GameStates.State_MainMenu);
	}
}
