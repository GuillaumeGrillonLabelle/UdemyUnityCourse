using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel(string name)
	{
		Debug.Log("New Level load: " + name);
		Brick.ResetBreakableCount();
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
		var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(nextLevelIndex);
			Brick.ResetBreakableCount();
		}
		else
		{
			LoadLevel("Win Screen");
		}
	}
}
