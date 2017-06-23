using UnityEngine;

public class Paddle : MonoBehaviour
{
	public bool autoPlay;
	public float paddleWidthInUnits;
	public float stageWidthInUnits;

	private Ball ball;

	private void Reset()
	{
		autoPlay = false;
		paddleWidthInUnits = 1;
		stageWidthInUnits = 16;
	}

	private void Start()
	{
		ball = FindObjectOfType<Ball>();
	}

	private void Update()
	{
		if (autoPlay)
			UpdateWithAutoPlay();
		else
			UpdateWithMouse();
	}

	void UpdateWithMouse()
	{
		var scale = transform.localScale.x;
		float mousePosInBlocks = Mathf.Clamp(
			Input.mousePosition.x / Screen.width * stageWidthInUnits,
			paddleWidthInUnits / 2 * scale,
			stageWidthInUnits - (paddleWidthInUnits / 2 * scale));

		transform.position = new Vector3(mousePosInBlocks, transform.position.y, transform.position.z);
	}

	void UpdateWithAutoPlay()
	{
		var scale = transform.localScale.x;
		float posX = Mathf.Clamp(
			ball.transform.position.x,
			paddleWidthInUnits / 2 * scale,
			stageWidthInUnits - (paddleWidthInUnits / 2 * scale));

		transform.position = new Vector3(posX, transform.position.y, transform.position.z);
	}
}
