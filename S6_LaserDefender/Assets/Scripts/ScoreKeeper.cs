using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	public delegate void ScoreChanged(int score);
	public event ScoreChanged OnScoreChanged;

	private int score;
	public int Score
	{
		get { return score; }
		private set
		{
			score = value;

			var ev = OnScoreChanged;
			if (ev != null)
				ev(score);
		}
	}

	static ScoreKeeper instance = null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		Reset();
	}

	public void Reset()
	{
		Score = 0;
	}

	public void AddPoints(int points)
	{
		Score += points;
	}
}
