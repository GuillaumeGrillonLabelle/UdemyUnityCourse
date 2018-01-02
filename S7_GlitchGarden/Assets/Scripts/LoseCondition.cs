using System.Collections;
using UnityEngine;

public class LoseCondition : Shredder {
    private LevelManager levelManager;
    private bool isGameOver;
    private GameObject winText;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        winText = GameObject.Find("LoseText");
        winText.SetActive(false);
    }

    protected new void OnTriggerEnter2D(Collider2D other)
    {
        if (!isGameOver)
        {
            base.OnTriggerEnter2D(other);
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        isGameOver = true;
        
        yield return new WaitForSecondsRealtime(1);

        winText.SetActive(true);

        yield return new WaitForSecondsRealtime(5);

        levelManager.LoadLevel("Lose Screen");

        Time.timeScale = 1;
    }

    void Update()
    {
        if (isGameOver)
        {
            Time.timeScale *= .99f;
        }
    }
}
