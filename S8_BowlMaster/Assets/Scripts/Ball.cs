using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public float launchVelocity = 200f;

    private Rigidbody rigibBody;
    private AudioSource audioSource;

    void Start ()
    {
        rigibBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        LaunchBall();
    }

    public void LaunchBall()
    {
        rigibBody.velocity = new Vector3(0, 0, launchVelocity);
        audioSource.Play();
    }
}
