using UnityEngine;

public class KillCollider : MonoBehaviour
{
	private LevelManager levelManager;

	private void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			levelManager.LoadLevel("Lose Screen");
		}
		else
		{
			Destroy(other.gameObject);
		}
	}
}
