using UnityEngine;

[CreateAssetMenu(fileName = "GatheringToolConfig", menuName = "ScriptableObjects/GatheringToolConfig", order = 2)]
public class GatheringToolConfig : ScriptableObject
{
    [Tooltip("Gathering animation ratio until end when crop will be gathered")]
    [Range(0f, 1f)]
    public float gatheringAnimationRatio = 1.0f;
}
