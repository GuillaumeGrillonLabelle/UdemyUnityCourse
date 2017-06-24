using UnityEngine;

public class Paddle : MonoBehaviour
{
	public bool autoPlay;
	public float paddleWidthInUnits;
	public float stageWidthInUnits;
	public Vector2 launchVelocity;

	private Ball ball;
	private bool ballLaunched;
	private Vector3 intialPaddleToBallVector;

	private void Reset()
	{
		autoPlay = false;
		paddleWidthInUnits = 1;
		stageWidthInUnits = 16;
		launchVelocity = new Vector2(2, 10);
	}

	private void Start()
	{
		ball = FindObjectOfType<Ball>();
		intialPaddleToBallVector = ball.transform.position - transform.position;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.A))
		{
			autoPlay = !autoPlay;
		}

		if (autoPlay && ballLaunched)
			UpdateWithAutoPlay();
		else
			UpdateWithMouse();
		UpdateBoost();
	}

	void UpdateWithMouse()
	{
		if (!ballLaunched)
		{
			ball.transform.position = transform.position + intialPaddleToBallVector;

			if (Input.GetMouseButtonDown(0))
			{
				ball.Launch(launchVelocity);
				ballLaunched = true;
			}
		}

		SetPosition(Input.mousePosition.x / Screen.width * stageWidthInUnits);
	}

	void UpdateWithAutoPlay()
	{
		SetPosition(ball.transform.position.x);
	}

	void SetPosition(float x)
	{
		var scale = transform.localScale.x;
		float posX = Mathf.Clamp(
			x,
			paddleWidthInUnits / 2 * scale,
			stageWidthInUnits - (paddleWidthInUnits / 2 * scale));

		transform.position = new Vector3(posX, transform.position.y, transform.position.z);
	}

	private void UpdateBoost()
	{
		var scale = transform.localScale;
		var isBoosting = Input.GetMouseButton(1);
		scale.y = isBoosting ? -1 : 1;
		transform.localScale = scale;
	}
}
