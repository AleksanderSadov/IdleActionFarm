using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPickupConfig", menuName = "ScriptableObjects/CharacterPickupConfig", order = 0)]
public class CharacterPickupConfig : ScriptableObject
{
    [Header("Pickup")]
    [Tooltip("Radius at which stack will start pooling up pickups")]
    public float poolUpRadius = 1f;
    [Tooltip("Speed at which pickup flies to stack")]
    public float poolUpSpeed = 1f;
    [Tooltip("Distance at which item considered picked up after pool up animation")]
    public float pickedUpDistance = 0.1f;
    public int maxStackSize = 10;
}
