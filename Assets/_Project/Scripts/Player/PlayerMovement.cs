using PersonalPackage.Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerConfiguration playerConfig;

    [Header("Thrust Particles")] [SerializeField]
    private ParticleSystem leftThrustParticles;

    [SerializeField] private ParticleSystem rightThrustParticles;
    private Vector3 rotationInput;

    private void Start()
    {
        inputReader.EnablePlayerInputActions();
        InitializeThruster(leftThrustParticles);
        InitializeThruster(rightThrustParticles);
    }


    private void FixedUpdate()
    {
        RotationBehaviour();
    }

    private void OnEnable()
    {
        inputReader.Move += OnRotationChanged;
    }

    private void OnDisable()
    {
        if (!inputReader) return;
        inputReader.Move -= OnRotationChanged;
    }

    private void OnRotationChanged(Vector2 direction)
    {
        rotationInput = direction;
        ThrustParticlesBehaviour();
    }

    private static void InitializeThruster(ParticleSystem particles)
    {
        if (!particles) return;
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.enabled = false;
        particles.Play();
    }


    private void RotationBehaviour()
    {
        transform.Rotate(-1 * playerConfig.RotationSpeed * rotationInput.x * Time.fixedDeltaTime * Vector3.forward);
    }

    private void ThrustParticlesBehaviour()
    {
        ParticleSystem.EmissionModule leftEmission = leftThrustParticles.emission;
        ParticleSystem.EmissionModule rightEmission = rightThrustParticles.emission;

        leftEmission.enabled = rotationInput.x > 0;
        rightEmission.enabled = rotationInput.x < 0;
    }
}