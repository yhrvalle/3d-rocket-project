using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private ParticleSystem mainThrust;

    private AudioSource audioSource;
    private Rigidbody rb;


    private bool thrustInput;

    private void Start()
    {
        thrustReader.EnablePlayerInputActions();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        ParticleSystem.EmissionModule emission = mainThrust.emission;
        emission.enabled = false;
        mainThrust.Play();
    }

    private void FixedUpdate()
    {
        ThrustBehaviour();
    }

    private void OnEnable()
    {
        thrustReader.Jump += OnThrustChanged;
    }

    private void OnDisable()
    {
        if (thrustReader) thrustReader.Jump -= OnThrustChanged;
    }

    private void OnThrustChanged(bool isThrusting)
    {
        thrustInput = isThrusting;
        MainThrustParticlesBehaviour(isThrusting);
        ThrustAudioBehaviour(isThrusting);
    }

    private void ThrustBehaviour()
    {
        if (!thrustInput) return;
        Vector3 finalThrust = playerConfig.ThrustForce * Time.fixedDeltaTime * Vector3.up;
        rb.AddRelativeForce(finalThrust);
    }

    private void MainThrustParticlesBehaviour(bool isThrusting)
    {
        ParticleSystem.EmissionModule emission = mainThrust.emission;
        emission.enabled = isThrusting;
    }

    private void ThrustAudioBehaviour(bool isActive)
    {
        if (isActive)
        {
            if (audioSource.isPlaying) return;
            audioSource.clip = playerConfig.ThrustSfx;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}