using UnityEngine;

public class Ball : MonoBehaviour
{
	private Rigidbody2D rb;
	private AudioSource bounceAudioSource;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
		bounceAudioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<Brick>() == null)
		{
			bounceAudioSource.Play();
		}

		if (other.gameObject.GetComponent<Paddle>() != null)
		{
			if (Mathf.Approximately(rb.velocity.x, 0f))
			{
				rb.velocity = new Vector2(Random.Range(-5f, 5f), rb.velocity.y);
			}
			if (Mathf.Approximately(rb.velocity.y, 0f))
			{
				rb.velocity = new Vector2(rb.velocity.x, 10f);
			}
		}
	}

	public void Launch(Vector2 launchVelocity)
	{
		rb.isKinematic = false;
		rb.velocity = launchVelocity;
	}
}
