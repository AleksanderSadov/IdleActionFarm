using StarterAssets;
using UnityEngine;

public class GatheringTool : MonoBehaviour
{
    [SerializeField]
    private ThirdPersonController player;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Crop") || !player.IsGathering())
        {
            return;
        }
    }
}
