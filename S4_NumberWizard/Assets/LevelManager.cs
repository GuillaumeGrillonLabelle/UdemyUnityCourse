using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel(string name)
	{
		print("LoadLevel: " + name);
		SceneManager.LoadScene(name);
	}

	public void Win()
	{
		LoadLevel("Win");
	}

	public void Loose()
	{
		LoadLevel("Loose");
	}

	public void Quit()
	{
		print("Quit");
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		 //Application.OpenURL(webplayerQuitURL);
#else
		 Application.Quit();
#endif
	}
}
