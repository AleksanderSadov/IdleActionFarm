using UnityEngine;

public class CropConfigUpdate : MonoBehaviour
{
    private Crop crop;
    private CropConfig config;
    private CropEffects cropEffects;
    private Vector3 grownedStateScale;
    private Vector3 randomScaleDeltaAmplitude;

    private void Start()
    {
        crop = GetComponent<Crop>();
        cropEffects = GetComponent<CropEffects>();
        config = crop.config;
    }

    private void Update()
    {
        UpdateChangeScale();
        UpdateChangeWindShakeDelta();
    }

    private void UpdateChangeScale()
    {
        if (
            randomScaleDeltaAmplitude != config.randomScaleDeltaAmplitude
            || grownedStateScale != config.grownedStateScale
        )
        {
            grownedStateScale = config.grownedStateScale;
            randomScaleDeltaAmplitude = config.randomScaleDeltaAmplitude;
            crop.grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        }

        if (crop.slicedStateScale != config.slicedStateScale)
        {
            crop.slicedStateScale = config.slicedStateScale;
        }

        if (crop.growingStateScale != config.growingStateScale)
        {
            crop.growingStateScale = config.growingStateScale;
        }
    }

    private void UpdateChangeWindShakeDelta()
    {
        if (cropEffects == null || !cropEffects.enabled)
        {
            return;
        }

        if (
            cropEffects.windShakeMinimumAmplitude != config.windShakeMinimumAmplitude
            || cropEffects.windShakeMaximumAmplitude != config.windShakeMaximumAmplitude
        )
        {
            transform.rotation = crop.originalRotation;
            cropEffects.windShakeMinimumAmplitude = config.windShakeMinimumAmplitude;
            cropEffects.windShakeMaximumAmplitude = config.windShakeMaximumAmplitude;
            cropEffects.windShakeRandomDelta = Random.Range(cropEffects.windShakeMinimumAmplitude, cropEffects.windShakeMaximumAmplitude);
        }
    }
}
