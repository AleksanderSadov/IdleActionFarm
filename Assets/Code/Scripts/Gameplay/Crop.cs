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

    private float timeGathered;
    private Quaternion originalRotation;

    private Vector3 originalGrownedStateScale;
    private Vector3 grownedStateScale;
    private Vector3 slicedStateScale;
    private Vector3 growingStateScale;
    private Vector3 randomScaleDeltaAmplitude;

    private bool isShakingBackward = true;
    private float windShakeMinimumAmplitude;
    private float windShakeMaximumAmplitude;
    private float windShakeRandomDelta;

    private void Start()
    {
        originalRotation = transform.rotation;
        grownedStateScale = config.grownedStateScale;
        slicedStateScale = config.slicedStateScale;
        growingStateScale = config.growingStateScale;
    }

    private void Update()
    {
        CheckChangeScale();
        UpdateCropStateTransitions();
        UpdateCropState();
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

    private void UpdateCropState()
    {
        switch (state)
        {
            case CropState.Growned:
                CheckChangeWindShakeDelta();
                ShakeLikeWind();
                break;
            case CropState.Sliced:
                CheckChangeWindShakeDelta();
                ShakeLikeWind();
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

    private void CheckChangeScale()
    {
        if (
            randomScaleDeltaAmplitude != config.randomScaleDeltaAmplitude
            || originalGrownedStateScale != config.grownedStateScale
        )
        {
            originalGrownedStateScale = config.grownedStateScale;
            randomScaleDeltaAmplitude = config.randomScaleDeltaAmplitude;
            grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        }

        if (slicedStateScale != config.slicedStateScale)
        {
            slicedStateScale = config.slicedStateScale;
        }

        if (growingStateScale != config.growingStateScale)
        {
            growingStateScale = config.growingStateScale;
        }
    }

    private void ShakeLikeWind()
    {
        float deltaAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, originalRotation.eulerAngles.z);

        if (Mathf.Abs(deltaAngle) >= windShakeRandomDelta)
        {
            isShakingBackward = !isShakingBackward;
        }

        if (isShakingBackward)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * config.windShakeSpeed);
        }
        else
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * config.windShakeSpeed);
        }
    }

    private void CheckChangeWindShakeDelta()
    {
        if (
            windShakeMinimumAmplitude != config.windShakeMinimumAmplitude
            || windShakeMaximumAmplitude != config.windShakeMaximumAmplitude
        )
        {
            transform.rotation = originalRotation;
            windShakeMinimumAmplitude = config.windShakeMinimumAmplitude;
            windShakeMaximumAmplitude = config.windShakeMaximumAmplitude;
            windShakeRandomDelta = Random.Range(windShakeMinimumAmplitude, windShakeMaximumAmplitude);
        }
    }
}
