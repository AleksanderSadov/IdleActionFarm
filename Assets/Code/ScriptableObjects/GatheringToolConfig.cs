using UnityEngine;

[CreateAssetMenu(fileName = "GatheringToolConfig", menuName = "ScriptableObjects/GatheringToolConfig", order = 3)]
public class GatheringToolConfig : ScriptableObject
{
    [Tooltip("Ratio at the start of gathering animation when crop will be gathered on collision")]
    [Range(0f, 1f)]
    public float gatheringAnimationRatio = 1.0f;
}
