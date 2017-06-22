using UnityEngine;

public class NumberWizard : MonoBehaviour
{
	int max;
	int min;
	int guess;

	void Start()
	{
		StartGame();
	}

	private void StartGame()
	{
		max = 1000;
		min = 1;
		guess = CalculateNextGuess();

		print("========================");
		print("Welcome to Number Wizard");
		print("Pick a number in your, but don't tell me!");

		print("The highest number you can pick is " + max + ".");
		print("The lowest number you can pick is " + min + ".");

		print("Is the number higher or lower than " + guess + "?");
		print("Up = higher, down = lower, return = equal.");

		max += 1;
	}

	void NextGuess()
	{
		guess = CalculateNextGuess();
		print("Higher or lower than " + guess);
	}

	int CalculateNextGuess()
	{
		return (max + min) / 2;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			min = guess;
			NextGuess();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			max = guess;
			NextGuess();
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			print("I won!");
			StartGame();
		}

	}
}
