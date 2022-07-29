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

    private float timeGathered;

    private void Update()
    {
        UpdateCropStateTransitions();
        UpdateSizeBasedOnState();
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
                transform.localScale = config.grownedStateScale;
                break;
            case CropState.Sliced:
                transform.localScale = config.slicedStateScale;
                break;
            case CropState.Growing:
                if (Time.time - timeGathered >= config.visibleGrowingDelay)
                {
                    transform.localScale = config.growingStateScale;
                }
                else
                {
                    transform.localScale = Vector3.zero;
                }
                break;
        }
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
}
