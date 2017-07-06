using UnityEngine;

public class SplashController : MonoBehaviour
{
	public float autoLoadStartScreenAfter = 1;
	public Fade fadeOut;

	void Start()
	{
		Invoke("FadeOut", autoLoadStartScreenAfter - fadeOut.fadeDuration);

		Invoke("LoadStartScreen", autoLoadStartScreenAfter);
	}

	private void FadeOut()
	{
		fadeOut.StartFade();
	}

	private void LoadStartScreen()
	{
		FindObjectOfType<LevelManager>().LoadLevel("Start Menu");
	}
}
