using UnityEngine;

public class Ball : MonoBehaviour
{
	public Vector2 launchVelocity;

	private Rigidbody2D rb;
	private AudioSource bounceAudioSource;
	private Paddle paddle;
	private Vector3 intialPaddleToBallVector;
	private bool ballLaunched;

	private void Reset()
	{
		launchVelocity = new Vector2(2, 10);
	}

	void Start()
	{
		paddle = FindObjectOfType<Paddle>();
		intialPaddleToBallVector = transform.position - paddle.transform.position;
		rb = GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
		bounceAudioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (!ballLaunched)
		{
			transform.position = paddle.transform.position + intialPaddleToBallVector;

			if (Input.GetMouseButtonDown(0))
			{
				rb.isKinematic = false;
				rb.velocity = launchVelocity;
				ballLaunched = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<Brick>() == null)
		{
			bounceAudioSource.Play();
		}
	}
}
