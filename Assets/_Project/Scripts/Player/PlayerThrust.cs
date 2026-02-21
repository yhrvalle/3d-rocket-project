using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private ParticleSystem[] thrustParticles;
    private AudioSource audioSource;
    private Rigidbody rb;


    private bool thrustInput;

    private void Start()
    {
        thrustReader.Jump += OnThrustChanged;
        thrustReader.EnablePlayerInputActions();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        foreach (ParticleSystem ps in thrustParticles)
        {
            ParticleSystem.EmissionModule emission = ps.emission;
            emission.enabled = false;
            ps.Play();
        }
    }

    private void FixedUpdate()
    {
        ThrustBehaviour();
    }

    private void OnThrustChanged(bool isThrusting)
    {
        thrustInput = isThrusting;
        ThrustParticlesBehaviour(isThrusting);
        ThrustAudioBehaviour(isThrusting);
    }

    private void ThrustBehaviour()
    {
        if (!thrustInput) return;
        Vector3 finalThrust = playerConfig.ThrustForce * Time.fixedDeltaTime * Vector3.up;
        rb.AddRelativeForce(finalThrust);
    }

    private void ThrustParticlesBehaviour(bool isThrusting)
    {
        foreach (ParticleSystem thrustParticle in thrustParticles)
        {
            ParticleSystem.EmissionModule emission = thrustParticle.emission;
            emission.enabled = isThrusting;
        }
    }

    private void ThrustAudioBehaviour(bool isActive)
    {
        if (isActive)
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(playerConfig.ThrustSfx);
        }
        else
        {
            audioSource.Stop();
        }
    }
}