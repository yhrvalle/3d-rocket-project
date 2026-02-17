using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Scriptable Objects/PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Player Settings")]
    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    public float ThrustForce => thrustForce;
    public float RotationSpeed => rotationSpeed;
}
