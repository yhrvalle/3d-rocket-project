using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    private bool thrustInput;
    private Rigidbody rb;
    private AudioSource audioSource;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        thrustReader.Jump += isThrusting => this.thrustInput = isThrusting;
        thrustReader.EnablePlayerInputActions();
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
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
