using UnityEngine;

public abstract class GenericSingleton<T> : Singleton where T : MonoBehaviour
{
	private static object _lock = new object();

	private static T _instance;
	public static T GetInstance
	{
		get
		{
			if (ApplicationIsQuitting)
			{
				Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
					"' will not be returned because the application is quitting.");
				return null;
			}

			lock (_lock)
			{
				if (_instance != null)
				{
					return _instance;
				}

				var instances = FindObjectsOfType<T>();

				if (instances.Length > 0)
				{
					if (instances.Length == 1)
					{
						return _instance = instances[0];
					}

					Debug.LogWarning("There should never be more than one Singleton of type '" +
						typeof(T) + "' in the scene, but " + instances.Length +
						" were found. The first instance found will be used, and all others destroyed.");

					for (var i = 1; i < instances.Length; i++)
					{
						Destroy(instances[i].gameObject);
					}

					return _instance = instances[0];
				}

				GameObject singleton = new GameObject();
				_instance = singleton.AddComponent<T>();
				singleton.name = "_" + typeof(T).ToString() + "(Singleton)";
				DontDestroyOnLoad(singleton);


				//Debug.Log("[Singleton] An instance of " + typeof(T) + 
				//" is needed in the scene, so '" + singleton + "' was created with DontDestroyOnLoad.");

				return _instance;
			}
		}
	}

	//protected virtual void Awake()
	//{
	//	if (_instance == null)
	//	{
	//		_instance = this as T;
	//	}
	//	else
	//	{
	//		Destroy(gameObject);
	//	}
	//	DontDestroyOnLoad(this.gameObject);
	//}
}

public abstract class Singleton : MonoBehaviour
{
	public static bool ApplicationIsQuitting { get; private set; }

	private void OnApplicationQuit()
	{
		ApplicationIsQuitting = true;
	}
}