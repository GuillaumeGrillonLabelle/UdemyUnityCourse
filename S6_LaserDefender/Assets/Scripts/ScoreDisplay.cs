using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

	private Text scoreTextElement;
	private ScoreKeeper scoreKeeper;

	void Start()
	{
		scoreTextElement = GetComponent<Text>();

		StartCoroutine(BindToScoreKeeper());
	}

	private IEnumerator BindToScoreKeeper()
	{
		yield return new WaitForEndOfFrame();

		scoreKeeper = FindObjectOfType<ScoreKeeper>();
		scoreKeeper.OnScoreChanged += ScoreKeeper_OnScoreChanged;

		scoreTextElement.text = scoreKeeper.Score.ToString();
	}

	private void ScoreKeeper_OnScoreChanged(int score)
	{
		scoreTextElement.text = score.ToString();
	}

	private void OnDestroy()
	{
		scoreKeeper.OnScoreChanged -= ScoreKeeper_OnScoreChanged;
	}
}
