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
    [Tooltip("Max pickup stack size")]
    public int maxStackSize = 10;

    [Header("Sell")]
    [Tooltip("Speed at which pickup flies to sell point")]
    public float sellingFlySpeed = 0.1f;
    [Tooltip("Delay in seconds between selling each pickup")]
    public float sellingDelay = 0.1f;
}
