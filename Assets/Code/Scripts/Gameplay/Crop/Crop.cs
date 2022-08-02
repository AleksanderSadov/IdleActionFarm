using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    public enum CropState
    {
        Growned,
        Sliced,
        Gathered,
        Growing,
    }

    public CropConfig config;
    public CropState state = CropState.Growned;
    public Vector3 grownedStateScale;
    public float timeGathered;

    public UnityAction cropGathered;

    private void Update()
    {
        UpdateCropStateTransitions();
        UpdateSizeBasedOnState();
    }

    public void GatherCrop()
    {
        switch (state)
        {
            case CropState.Growned:
                state = CropState.Sliced;
                break;
            case CropState.Sliced:
                state = CropState.Gathered;
                timeGathered = Time.time;
                SpawnCropPickup();
                cropGathered?.Invoke();
                break;
        }
    }

    private void UpdateCropStateTransitions()
    {
        switch (state)
        {
            case CropState.Gathered:
                state = CropState.Growing;
                break;
            case CropState.Growing:
                if (Time.time - timeGathered >= config.fullGrowTime)
                {
                    state = CropState.Growned;
                }
                break;
        }
    }

    private void UpdateSizeBasedOnState()
    {
        switch (state)
        {
            case CropState.Growned:
                if (transform.localScale != grownedStateScale && !DOTween.IsTweening(transform))
                {
                    transform.DOScale(grownedStateScale, config.animationFullGrowingSpeed);
                }
                break;
            case CropState.Sliced:
                if (transform.localScale != config.slicedStateScale)
                {
                    transform.localScale = config.slicedStateScale;
                }
                break;
            case CropState.Growing:
                if (Time.time - timeGathered >= config.smallGrowingDelay)
                {
                    if (transform.localScale != config.growingStateScale && !DOTween.IsTweening(transform))
                    {
                        transform.DOScale(config.growingStateScale, config.animationSmallGrowingSpeed);
                    }
                }
                else
                {
                    if (transform.localScale != Vector3.zero)
                    {
                        transform.localScale = Vector3.zero;
                    }
                }
                break;
        }
    }

    private void SpawnCropPickup()
    {
        Vector3 pickupSize = config.pickupPrefab.transform.localScale;

        CropPickup cropPickup = Instantiate(
            config.pickupPrefab,
            new Vector3(transform.position.x, transform.position.y + pickupSize.y / 2, transform.position.z),
            config.pickupPrefab.transform.rotation
        );
    }
}
