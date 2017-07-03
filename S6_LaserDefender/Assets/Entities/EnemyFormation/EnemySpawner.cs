using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float speed;
	public float spawnDelay;

	private float minX;
	private float maxX;
	private bool movingRight;

	void Start()
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

		minX = leftBoundary.x;
		maxX = rightBoundary.x;

		SpawnUntillFull();
	}

	private void SpawnWave()
	{
		foreach (Transform child in transform)
		{
			Instantiate(enemyPrefab, child);
		}
	}

	void SpawnUntillFull()
	{
		Transform freePosition = NextFreePosition();
		if (freePosition != null)
		{
			Instantiate(enemyPrefab, freePosition);
		}

		if (NextFreePosition())
		{
			Invoke("SpawnUntillFull", spawnDelay);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	// Update is called once per frame
	void Update()
	{
		var dir = movingRight ? Vector3.right : Vector3.left;
		transform.position += dir * speed * Time.deltaTime;

		if (transform.position.x - width / 2f < minX)
		{
			movingRight = true;
		}
		else if (transform.position.x + width / 2f > maxX)
		{
			movingRight = false;
		}

		if (AllMembersAreDead())
		{
			SpawnUntillFull();
		}
	}

	private Transform NextFreePosition()
	{
		foreach (Transform child in transform)
		{
			if (child.childCount == 0)
				return child;
		}

		return null;
	}

	private bool AllMembersAreDead()
	{
		foreach (Transform child in transform)
		{
			if (child.childCount > 0)
				return false;
		}

		return true;
	}
}
