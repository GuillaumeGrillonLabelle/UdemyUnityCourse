using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
	public GameObject[] attackerPrefabs;

	void Update()
	{
		foreach (var attackerPrefab in attackerPrefabs)
		{
			if (IsTimeToSpawn(attackerPrefab))
			{
				Spawn(attackerPrefab);
			}
		}
	}

	private bool IsTimeToSpawn(GameObject attackerPrefab)
	{
		var attacker = attackerPrefab.GetComponent<Attacker>();

		float meanSpawnDelay = attacker.seenEverySeconds;
		float spawnsPerSeconds = 1 / meanSpawnDelay;

		if (Time.deltaTime > meanSpawnDelay)
		{
			Debug.LogWarning("Spawn rate capped by frame rate");
		}

		float threshold = spawnsPerSeconds * Time.deltaTime / 5;

		return Random.value < threshold;
	}

	private void Spawn(GameObject attackerPrefab)
	{
		var attacker = Instantiate(attackerPrefab);
		attacker.transform.parent = transform;
		attacker.transform.position = transform.position;
		attacker.name += " " + attacker.GetInstanceID();
	}
}
