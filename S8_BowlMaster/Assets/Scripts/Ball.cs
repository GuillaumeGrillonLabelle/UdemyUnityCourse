using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ball : MonoBehaviour {
    [HideInInspector] public bool inPlay;

    private Rigidbody rigibBody;
    private AudioSource audioSource;

    void Start ()
    {
        rigibBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        inPlay = false;
        rigibBody.isKinematic = true;
    }

    public void ResetBall(Vector3 position)
    {
        inPlay = false;
        rigibBody.isKinematic = true;
        transform.position = position;
        audioSource.Stop();
    }

    public void LaunchBall(float delay, Vector3 velocity)
    {
        StartCoroutine(LaunchBallCoroutine(delay, velocity));
    }

    private IEnumerator LaunchBallCoroutine(float delay, Vector3 velocity)
    {
        inPlay = true;

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
