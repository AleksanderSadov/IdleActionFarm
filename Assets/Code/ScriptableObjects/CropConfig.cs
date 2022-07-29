using UnityEngine;

[CreateAssetMenu(fileName = "CropConfig", menuName = "ScriptableObjects/CropConfig", order = 1)]
public class CropConfig : ScriptableObject
{
    public Vector3 grownedStateScale;
    public Vector3 slicedStateScale;
    public Vector3 growingStateScale;

    [Tooltip("Time in seconds crop fully grows back again from gathered state")]
    public float fullGrowTime;
    [Tooltip("Delay in seconds crop displays visible small growing state after being gathered")]
    public float smallGrowingDelay;
    [Tooltip("Speed at which crop animation goes from gathered to small growing state")]
    public float animationSmallGrowingSpeed;
    [Tooltip("Speed at which crop animation goes from small to full grown state")]
    public float animationFullGrowingSpeed;

    public float windShakeSpeed = 3.0f;
    public float windShakeMinimumAmplitude = 2.0f;
    public float windShakeMaximumAmplitude = 4.0f;
}
