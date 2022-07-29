using DG.Tweening;
using UnityEngine;

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
    public float timeGathered;

    public Vector3 grownedStateScale;
    public Vector3 slicedStateScale;
    public Vector3 growingStateScale;
    public Quaternion originalRotation;

    private void Start()
    {
        grownedStateScale = config.grownedStateScale;
        slicedStateScale = config.slicedStateScale;
        growingStateScale = config.growingStateScale;
        originalRotation = transform.rotation;
    }

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
                if (transform.localScale != slicedStateScale)
                {
                    transform.localScale = slicedStateScale;
                }
                break;
            case CropState.Growing:
                if (Time.time - timeGathered >= config.smallGrowingDelay)
                {
                    if (transform.localScale != growingStateScale && !DOTween.IsTweening(transform))
                    {
                        transform.DOScale(growingStateScale, config.animationSmallGrowingSpeed);
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
}
