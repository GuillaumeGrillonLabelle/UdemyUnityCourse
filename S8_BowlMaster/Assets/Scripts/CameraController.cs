using UnityEngine;

public class CameraController : MonoBehaviour {
    public Ball ball;
    public Vector3 offset = new Vector3(0, 34.8f, -95.45f);
    public float maxFollowZ = 1829f;

	void Update () {
        if (transform.position.z < maxFollowZ)
        {
            transform.position = ball.transform.position + offset;
        }
	}
}
