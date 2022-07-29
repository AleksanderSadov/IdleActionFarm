using StarterAssets;
using UnityEngine;

public class GatheringTool : MonoBehaviour
{
    public GatheringToolConfig config;

    private ThirdPersonController player;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        player = GetComponentInParent<ThirdPersonController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.enabled = player.IsGathering();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (
            !other.CompareTag("Crop")
            || !player.IsGathering()
            || player.GetGatheringAnimatorTime() > config.gatheringAnimationRatio
        )
        {
            return;
        }

        other.GetComponent<Crop>().GatherCrop();
    }
}
