using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {
    public float delayBeforeLaunchingTheBall = 0.5f;

    private Ball ball;
    private float startDragTime;
    private Vector3 startDragPosition;

    void Start () {
        ball = GetComponent<Ball>();
    }

    public void DragStart()
    {
        startDragTime = Time.time;
        startDragPosition = Input.mousePosition;
    }

    public void DragEnd()
    {
        var dragDuration = Time.time - startDragTime;
        var dragVector = Input.mousePosition - startDragPosition;
        var launchVelocity = new Vector3(dragVector.x, 0, dragVector.y) / dragDuration;

        ball.LaunchBall(delayBeforeLaunchingTheBall, launchVelocity);
    }
}
