using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour
{
	public int max;
	public int min;
	public int maxGuessesAllowed;
	public Text isYourNumberTextElement;
	public Text guessTextElement;
	public Text thinkingTextElement;
	public LevelManager levelManager;

	private int guess;

	//Default values
	private void Reset()
	{
		max = 1000;
		min = 1;
		maxGuessesAllowed = 10;
	}

	void Start()
	{
		StartCoroutine(StartGameScript());
	}

	private IEnumerator StartGameScript()
	{
		yield return NextGuessScript();


	}

	public void GuessHigher()
	{
		min = guess;
		NextGuess();
	}

	public void GuessLower()
	{
		max = guess;
		NextGuess();
	}

	public void NumberFound()
	{
		StartCoroutine(NumberFoundScript());
	}

	private IEnumerator NumberFoundScript()
	{
		DisableAllButtonsInTheScene(true);

		yield return new WaitForSeconds(1.5f);

		levelManager.Loose();
	}

	private static void EnableAllButtonsInTheScene()
	{
		var buttons = FindObjectsOfType<Button>();
		foreach (var b in buttons)
		{
			b.interactable = true;
		}
	}

	private static void DisableAllButtonsInTheScene(bool alsoDisableBackButton)
	{
		var buttons = FindObjectsOfType<Button>();
		foreach (var b in buttons)
		{
			if (alsoDisableBackButton || !b.CompareTag("BackButton"))
			{
				b.interactable = false;
			}
		}
	}

	void NextGuess()
	{
		StartCoroutine(NextGuessScript());
	}

	private IEnumerator NextGuessScript()
	{
		DisableAllButtonsInTheScene(false);

		isYourNumberTextElement.enabled = false;

		guessTextElement.enabled = false;

		yield return ThinkingScript();

		yield return new WaitForSeconds(.25f);

		guess = CalculateNextGuess();
		guessTextElement.enabled = true;
		guessTextElement.text = guess.ToString();

		isYourNumberTextElement.enabled = true;

		EnableAllButtonsInTheScene();

		if (--maxGuessesAllowed <= 0)
		{
			yield return NoMoreTryPlayerWins();
		}
	}

	private IEnumerator ThinkingScript()
	{
		thinkingTextElement.enabled = true;
		const float waitTime = .15f;
		for (int i = 0; i < 2; i++)
		{
			Think("");
			yield return new WaitForSeconds(waitTime);
			Think(".");
			yield return new WaitForSeconds(waitTime);
			Think(". .");
			yield return new WaitForSeconds(waitTime);
			Think(". . .");
			yield return new WaitForSeconds(waitTime);
		}
		thinkingTextElement.enabled = false;
	}

	private void Think(string dots)
	{
		thinkingTextElement.text = "A number between " + min + " and " + max + "\n" + dots;
	}

	private IEnumerator NoMoreTryPlayerWins()
	{
		DisableAllButtonsInTheScene(true);

		yield return new WaitForSeconds(1.5f);

		levelManager.Win();
	}

	int CalculateNextGuess()
	{
		return Random.Range(min, max + 1);
	}
}
