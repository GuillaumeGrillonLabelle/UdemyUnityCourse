using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameTimer : MonoBehaviour {
    public int levelDurationInSeconds = 60;

    private Slider slider;
    private LevelManager levelManager;
    private bool isGameOver;
    private AudioSource audioSource;
    private GameObject winText;

    void Start () {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;

        levelManager = FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();

        winText = GameObject.Find("WinText");
        winText.SetActive(false);
    }
	
	void Update () {
		if (Time.timeSinceLevelLoad <= levelDurationInSeconds)
        {
            slider.value = Time.timeSinceLevelLoad / levelDurationInSeconds;
        }
        else if (!isGameOver)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            Time.timeScale *= .99f;
        }
	}

    private IEnumerator GameOver()
    {
        isGameOver = true;


        var loseCondition = FindObjectOfType<LoseCondition>();
        if (loseCondition != null)
        {
            loseCondition.enabled = false;
        }

        yield return new WaitForSecondsRealtime(1);

        audioSource.Play();

        winText.SetActive(true);

        yield return new WaitForSecondsRealtime(5);

        levelManager.LoadNextLevel();

        Time.timeScale = 1;
    }
}
