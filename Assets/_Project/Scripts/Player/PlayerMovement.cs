using UnityEngine;
using PersonalPackage.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerConfiguration playerConfig;
    private Vector3 rotationInput;

    private void Start()
    {
        inputReader.Move += direction => this.rotationInput = direction;
        inputReader.EnablePlayerInputActions();
        
    }
    private void FixedUpdate()
    {
        RotationBehaviour();
    }
    private void RotationBehaviour()
    {
        transform.Rotate(-1 * playerConfig.RotationSpeed * rotationInput.x * Time.fixedDeltaTime * Vector3.forward);
    }
}
