using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {
    [SerializeField] private float delayBeforeLaunchingTheBall = 0.5f;
    [SerializeField] private float minDragLength = 50f;


    private Ball ball;
    private Vector3 initialPosition;
    private Vector3 startPositionOffset;
    private float startDragTime;
    private Vector3 startDragPosition;
    
    void Start ()
    {
        ball = GetComponent<Ball>();
        
        StartCoroutine(InitBall());
    }

    private IEnumerator InitBall()
    {
        yield return null;
        
        initialPosition = transform.position;
        startPositionOffset = Vector3.zero;
        ResetBall();

        yield return null;
    }

    public void ResetBall()
    {
        ball.ResetBall(initialPosition + startPositionOffset);
    }

    public void DragStart()
    {
        ResetBall();

        startDragTime = Time.time;
        startDragPosition = Input.mousePosition;
    }

    public void DragEnd()
    {
        var dragVector = Input.mousePosition - startDragPosition;
        if (dragVector.magnitude > minDragLength)
        {
            var dragDuration = Time.time - startDragTime;
            var launchVelocity = new Vector3(dragVector.x, 0, dragVector.y) / dragDuration;

            ball.LaunchBall(delayBeforeLaunchingTheBall, launchVelocity);
        }
    }

    public void MoveStart(float nudge)
    {
        if (!ball.inPlay)
        {
            startPositionOffset += new Vector3(nudge, 0, 0);
            ResetBall();
        }
    }
}
