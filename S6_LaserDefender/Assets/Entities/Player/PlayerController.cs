using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public GameObject laserPrefab;
	public float projectileSpeed;
	public float firingRate;
	public AudioClip fireSound;
	public float health;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	private void Reset()
	{
		speed = 10f;
	}

	void Start()
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		var bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		var topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));
		var extents = GetComponent<SpriteRenderer>().bounds.extents;

		minX = bottomLeft.x + extents.x;
		maxX = topRight.x - extents.x;
		minY = bottomLeft.y + extents.y;
		maxY = topRight.y - extents.y;
	}

	void Update()
	{
		Movement();
		ClampPosition();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", Mathf.Epsilon, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
	}

	private void Movement()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
	}

	private void ClampPosition()
	{
		var clampedPosition = new Vector3(
			Mathf.Clamp(transform.position.x, minX, maxX),
			Mathf.Clamp(transform.position.y, minY, maxY),
			transform.position.z
		);
		transform.position = clampedPosition;
	}

	private void Fire()
	{
		var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
		laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		var projectile = collider.gameObject.GetComponent<Projectile>();
		if (projectile)
		{
			health -= projectile.getDamage();
			if (health <= 0)
			{
				Die();
			}
			projectile.Hit();
		}
	}

	private void Die()
	{
		var levelManager = FindObjectOfType<LevelManager>();
		levelManager.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}
