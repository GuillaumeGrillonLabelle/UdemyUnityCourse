using System.Collections;
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
		StartCoroutine(YouWinGoToNextLevelScript());
	}

	private IEnumerator YouWinGoToNextLevelScript()
	{

		//Slow the ball
		float timeScale = 0.25f;
		Time.timeScale *= timeScale;

		yield return new WaitForSecondsRealtime(.5f);

		//Stop the ball
		var ball = FindObjectOfType<Ball>();
		ball.GetComponent<Rigidbody2D>().isKinematic = true;
		ball.GetComponent<CircleCollider2D>().enabled = false;

		yield return new WaitForSecondsRealtime(.75f);

		Time.timeScale /= timeScale;

		DoLoadNextLevel();
	}

	private void DoLoadNextLevel()
	{
		var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
		{
			Brick.ResetBreakableCount();
			SceneManager.LoadScene(nextLevelIndex);
		}
		else
		{
			LoadLevel("Win Screen");
		}
	}
}
