using UnityEngine;

public class LoseCondition : Shredder {
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    protected new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        levelManager.LoadLevel("Lose Screen");
    }
}
