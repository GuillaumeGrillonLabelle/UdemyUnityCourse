using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float moveSpeed;
	public float spinSpeed;
	public float damage;

	private Transform body;

	void Start()
	{
		body = transform.Find("Body");
	}

	void Update()
	{
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		body.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var attacker = other.GetComponent<Attacker>();
		if (attacker != null)
		{
			attacker.ApplyDamage(damage);

			Destroy(gameObject);
		}
	}
}
