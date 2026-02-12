using PersonalPackage.Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
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

    private void Update()
    {
        Debug.Log($"Thrust Input: {thrustInput}");
    }
}
