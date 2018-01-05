using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Ball : MonoBehaviour {
    public Vector3 launchVelocity = new Vector3(0, 0, 200f);

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
        rigibBody.velocity = launchVelocity;
        audioSource.Play();
    }
}
