using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Scriptable Objects/PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Player Settings")]
    [SerializeField] private float thrustForce = 10f;
    public float ThrustForce => thrustForce;
}
