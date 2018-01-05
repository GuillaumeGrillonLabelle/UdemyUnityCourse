using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public Vector3 launchVelocity = new Vector3(0, 0, 1500f);

    private Rigidbody rigibBody;
    private AudioSource audioSource;
    private Vector3 initialPosition;

    void Start ()
    {
        rigibBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        
        rigibBody.isKinematic = true;
    }

    public void LaunchBall(float delay, Vector3 velocity)
    {
        StartCoroutine(LaunchBallCoroutine(delay, velocity));
    }

    private IEnumerator LaunchBallCoroutine(float delay, Vector3 velocity)
    {
        rigibBody.isKinematic = true;
        transform.position = initialPosition;
        audioSource.Stop();

        yield return new WaitForSeconds(delay);

        DoLaunchBall(velocity);
    }

    private void DoLaunchBall(Vector3 velocity)
    {
        rigibBody.isKinematic = false;
        rigibBody.velocity = velocity;
        audioSource.Play();
    }
}
