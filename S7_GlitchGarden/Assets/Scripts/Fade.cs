using UnityEngine;
using UnityEngine.UI;

public enum FadeType { FadeIn, FadeOut }

public class Fade : MonoBehaviour
{
	public float fadeDuration;
	public bool triggerOnStart;
	public FadeType fadeType;
	public bool disableGameObjectAtEndOfFade;

	private Image image;
	private bool isFading;
	private float fadeStartTime;

	private void Reset()
	{
		fadeDuration = 1f;
		triggerOnStart = true;
		fadeType = FadeType.FadeOut;
		disableGameObjectAtEndOfFade = true;
	}

	void Start()
	{
		image = GetComponent<Image>();
		isFading = false;

		if (triggerOnStart)
		{
			StartFade();
		}
		else
		{
			enabled = false;
		}
	}

	public void StartFade()
	{
		isFading = true;
		fadeStartTime = Time.realtimeSinceStartup;
		image.enabled = true;
		enabled = true;
	}

	void Update()
	{
		if (isFading)
		{
			var color = image.color;

			var deltaTime = Time.realtimeSinceStartup - fadeStartTime;
			var lerpAlpha = deltaTime / fadeDuration;
			float startAlpha = fadeType == FadeType.FadeOut ? 1f : 0f;
			float endAlpha = fadeType == FadeType.FadeOut ? 0f : 1f;
			color.a = Mathf.Lerp(startAlpha, endAlpha, lerpAlpha);

			image.color = color;

			if (lerpAlpha >= 1f)
			{
				if (disableGameObjectAtEndOfFade)
				{
					gameObject.SetActive(false);
				}
				isFading = false;
				enabled = false;
			}
		}
	}
}
