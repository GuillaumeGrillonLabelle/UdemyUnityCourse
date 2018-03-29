using UnityEngine;

public class CameraController : MonoBehaviour {
    public Ball ball;
    public float maxFollowZ = 1829f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

	void LateUpdate () {
        if (ball.transform.position.z < maxFollowZ)
        {
            transform.position = ball.transform.position + offset;
        }
	}
}
