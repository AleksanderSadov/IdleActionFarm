using StarterAssets;
using UnityEngine;

public class GatheringTool : MonoBehaviour
{
    [SerializeField]
    private ThirdPersonController player;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.enabled = player.IsGathering();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Crop") || !player.IsGathering())
        {
            return;
        }
    }
}
