using System.Collections;
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
			StartCoroutine(YouLoseScript());
		}
		else
		{
			Destroy(other.gameObject);
		}
	}

	private IEnumerator YouLoseScript()
	{
		yield return new WaitForSeconds(.75f);

		levelManager.LoadLevel("Lose Screen");
	}
}
