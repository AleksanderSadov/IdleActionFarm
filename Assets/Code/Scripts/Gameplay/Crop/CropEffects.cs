using DG.Tweening;
using UnityEngine;
using static Crop;

[RequireComponent(typeof(Crop))]
public class CropEffects : MonoBehaviour
{
    private Crop crop;
    private CropConfig config;
    private bool isShakingBackward = true;
    private Quaternion originalRotation;
    private float windShakeRandomDelta;
    private int currentAgeIndex = 0;
    private Material cropMaterial;

    private void Awake()
    {
        crop = GetComponent<Crop>();
        cropMaterial = crop.GetComponent<MeshRenderer>().material;
        config = crop.config;
        originalRotation = transform.rotation;

        crop.grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        windShakeRandomDelta = Random.Range(config.windShakeMinimumAmplitude, config.windShakeMaximumAmplitude);
    }

    private void OnEnable()
    {
        crop.cropDamaged += OnCropDamaged;
        crop.cropGathered += OnCropGathered;
    }

    private void Update()
    {
        UpdateCropEffects();
    }

    private void OnDisable()
    {
        crop.cropDamaged -= OnCropDamaged;
        crop.cropGathered -= OnCropGathered;
    }

    private void UpdateCropEffects()
    {
        switch (crop.state)
        {
            case CropState.Growned:
                ShakeLikeWind();
                Aging();
                break;
            case CropState.Sliced:
                ShakeLikeWind();
                Aging();
                break;
        }
    }

    private void OnCropGathered()
    {
        transform.rotation = originalRotation;

        if (config.agingColors.Length > 0)
        {
            if (DOTween.IsTweening(cropMaterial))
            {
                DOTween.Kill(cropMaterial);
            }

            currentAgeIndex = 0;
            cropMaterial.color = config.agingColors[0];
        }
    }

    private void OnCropDamaged()
    {
        MiscAudio.PlayClipAtPoint(
            config.slicedAudioClip,
            transform.position,
            1f,
            config.slicedAudioPitchRange,
            config.slicedAudioGroup
        );
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

    private void Aging()
    {
        if (
            config.agingColors.Length == 0
            || currentAgeIndex >= config.agingColors.Length - 1
            || DOTween.IsTweening(cropMaterial)
        )
        {
            return;
        }

        currentAgeIndex++;
        cropMaterial.DOColor(config.agingColors[currentAgeIndex], config.agingInterval);
    }
}
