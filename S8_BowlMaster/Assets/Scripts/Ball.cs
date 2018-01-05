using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public int delayBeforeLaunchingTheBall = 1;
    public Vector3 launchVelocity = new Vector3(0, 0, 1500f);

    private Rigidbody rigibBody;
    private AudioSource audioSource;

    void Start ()
    {
        rigibBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
        LaunchBall(delayBeforeLaunchingTheBall);
    }

    public void LaunchBall(int delay)
    {
        StartCoroutine(LaunchBallCoroutine(delay));
    }

    private IEnumerator LaunchBallCoroutine(int delay)
    {
        rigibBody.isKinematic = true;

        yield return new WaitForSeconds(delay);

        DoLaunchBall();
    }

    public void DoLaunchBall()
    {
        rigibBody.isKinematic = false;
        rigibBody.velocity = launchVelocity;
        audioSource.Play();
    }
}
