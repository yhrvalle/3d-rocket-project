using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private AudioClip thrustAudioClip;
    private bool thrustInput;
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        thrustReader.Jump += isThrusting => this.thrustInput = isThrusting;
        thrustReader.EnablePlayerInputActions();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ThrustBehaviour();
    }

    private void ThrustBehaviour()
    {
        if (thrustInput)
        {
            Vector3 finalThrust = playerConfig.ThrustForce * Time.fixedDeltaTime * Vector3.up;
            rb.AddRelativeForce(finalThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustAudioClip);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
