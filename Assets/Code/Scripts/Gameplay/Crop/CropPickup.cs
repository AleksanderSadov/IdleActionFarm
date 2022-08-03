using UnityEngine;

public class CropPickup : MonoBehaviour
{
    public CropPickupConfig config;
    public CropConfig cropSellingCostConfig;
    public bool isPickingUpInProgress = false;
    public bool isSellingInProgress = false;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        LaunchForce();
    }

    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    private void LaunchForce()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(0f, config.launchRandomDirection.x) * MiscRandom.GetRandomSign(),
            config.launchRandomDirection.y,
            Random.Range(0f, config.launchRandomDirection.z) * MiscRandom.GetRandomSign()
        );

        rigidBody.AddForce(
            (config.launchBaseDirection + randomDirection) * config.launchForce,
            ForceMode.Impulse
        );
    }
}
