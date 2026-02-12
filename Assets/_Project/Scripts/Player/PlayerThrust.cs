using PersonalPackage.Input;
using UnityEngine;

public class PlayerThrust : MonoBehaviour
{
    [SerializeField] private PlayerInputReader thrustReader;
    private bool thrustInput;

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
