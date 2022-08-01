using UnityEngine;

[CreateAssetMenu(fileName = "CropPickupConfig", menuName = "ScriptableObjects/CropPickupConfig", order = 2)]
public class CropPickupConfig : ScriptableObject
{
    [Header("Launch")]
    [Tooltip("Base launch direction when pickup is spawned")]
    public Vector3 launchBaseDirection = Vector3.up;
    [Tooltip("Random vector will be generated within this max limit and added to base direction")]
    public Vector3 launchRandomDirection = new Vector3(1, 0, 1);
    [Tooltip("Launch force when pickup is spawned")]
    public float launchForce = 1.0f;
}
