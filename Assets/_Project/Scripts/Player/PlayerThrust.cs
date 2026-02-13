using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    private bool thrustInput;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }
}
