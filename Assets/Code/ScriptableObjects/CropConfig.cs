using UnityEngine;

[CreateAssetMenu(fileName = "CropConfig", menuName = "ScriptableObjects/CropConfig", order = 1)]
public class CropConfig : ScriptableObject
{
    [Header("Size")]
    public Vector3 grownedStateScale = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 slicedStateScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 growingStateScale = new Vector3(0.1f, 0.1f, 0.1f);
    [Tooltip("Change crop scale at small random amplitude to appear more unique")]
    public Vector3 randomScaleDeltaAmplitude = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Growing speed")]
    [Tooltip("Time in seconds crop fully grows back again from gathered state")]
    public float fullGrowTime = 10.0f;
    [Tooltip("Delay in seconds crop displays visible small growing state after being gathered")]
    public float smallGrowingDelay = 2.0f;
    [Tooltip("Speed at which crop animation goes from gathered to small growing state")]
    public float animationSmallGrowingSpeed = 1.0f;
    [Tooltip("Speed at which crop animation goes from small to full grown state")]
    public float animationFullGrowingSpeed = 0.4f;

    [Header("Pickup")]
    [Tooltip("Prefab for pickup which appears after crop is gathered")]
    public CropPickup pickupPrefab;

    [Header("Selling")]
    [Tooltip("Selling cost when crop is gathered and delivered")]
    public int sellingCost;

    [Header("Color")]
    [Tooltip("Possible materials for crop. Single material will be choosed randomly for each crop")]
    public Material[] materials;

    [Header("Wind animation")]
    public float windShakeSpeed = 3.0f;
    public float windShakeMinimumAmplitude = 2.0f;
    public float windShakeMaximumAmplitude = 4.0f;
}
