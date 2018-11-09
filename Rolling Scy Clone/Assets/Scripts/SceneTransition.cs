using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour {

	public static SceneTransition instance;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		}
		if (instance != this) {
			Destroy (this.gameObject);
		}
	}

	void Start()
	{
		DontDestroyOnLoad (this.gameObject);
	}
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);

	}
}
