using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel(string name)
	{
		Debug.Log("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest()
	{
		Debug.Log("Quit requested");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		 //Application.OpenURL(webplayerQuitURL);
#else
		 Application.Quit();
#endif
	}

	public void LoadNextLevel()
	{
		StartCoroutine(LoadNextLevelScript());
	}

	private IEnumerator LoadNextLevelScript()
	{
		yield return new WaitForSecondsRealtime(.75f);

		var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(nextLevelIndex);
		}
		else
		{
			LoadLevel("Win Screen");
		}
	}
}
