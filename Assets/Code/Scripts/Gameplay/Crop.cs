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

    private void Start()
    {

    }

    private void Update()
    {
        UpdateCropStateTransitions();
        UpdateCropState();
        UpdateSizeBasedOnState();
    }

    private void UpdateCropStateTransitions()
    {

    }

    private void UpdateCropState()
    {

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
            case CropState.Gathered:
                transform.localScale = config.gatheredStateScale;
                break;
            case CropState.Growing:
                transform.localScale = config.growingStateScale;
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
                break;
        }
    }
}
