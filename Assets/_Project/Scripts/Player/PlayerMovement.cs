using UnityEngine;
using PersonalPackage.Input;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    private Vector3 moveInput;

    private void Start()
    {
        inputReader.Move += direction => this.moveInput = direction;
        inputReader.EnablePlayerInputActions();
    }
    void Update()
    {
        Debug.Log($"Move Input: {moveInput}");
    }
}
