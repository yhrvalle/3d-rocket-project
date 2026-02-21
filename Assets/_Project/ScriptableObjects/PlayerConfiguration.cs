using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Scriptable Objects/PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Player Settings")]
    [SerializeField] private float thrustForce = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    public float ThrustForce => thrustForce;
    public float RotationSpeed => rotationSpeed;
    
    [Header("SFX Settings")] 
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioClip successSfx;
    [SerializeField] private AudioClip thrustSfx;

    public AudioClip CrashSfx => crashSfx;
    public AudioClip SuccessSfx => successSfx;
    public AudioClip ThrustSfx => thrustSfx;
    
}
