using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float health = 150f;
	public GameObject laserPrefab;
	public float projectileSpeed;
	public float shotsPerSeconds;
	public int pointsValue;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		var projectile = collider.gameObject.GetComponent<Projectile>();
		if (projectile)
		{
			health -= projectile.getDamage();
			if (health <= 0)
			{
				Destroy(gameObject);
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				FindObjectOfType<ScoreKeeper>().AddPoints(pointsValue);
			}
			projectile.Hit();
		}
	}

	private void Update()
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability)
		{
			Fire();
		}
	}

	private void Fire()
	{
		var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
		laser.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
}
