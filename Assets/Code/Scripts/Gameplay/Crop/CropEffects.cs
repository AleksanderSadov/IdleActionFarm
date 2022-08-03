using UnityEngine;
using static Crop;

[RequireComponent(typeof(Crop))]
public class CropEffects : MonoBehaviour
{
    private Crop crop;
    private CropConfig config;
    private bool isShakingBackward = true;
    private Quaternion originalRotation;
    private float timeLastAgeChange;
    private float windShakeRandomDelta;
    private int currentAgeIndex = 0;

    private void Awake()
    {
        crop = GetComponent<Crop>();
        config = crop.config;
        originalRotation = transform.rotation;
        timeLastAgeChange = Time.time;

        crop.grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        windShakeRandomDelta = Random.Range(config.windShakeMinimumAmplitude, config.windShakeMaximumAmplitude);
    }

    private void OnEnable()
    {
        crop.cropGrowned += OnCropGrowned;
        crop.cropDamaged += OnCropDamaged;
        crop.cropGathered += OnCropGathered;
    }

    private void Update()
    {
        UpdateCropEffects();
    }

    private void OnDisable()
    {
        crop.cropGrowned -= OnCropGrowned;
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

    private void OnCropGrowned()
    {
        timeLastAgeChange = Time.time;
    }

    private void OnCropGathered()
    {
        transform.rotation = originalRotation;

        currentAgeIndex = 0;
        if (config.agingMaterials.Length > 0)
        {
            crop.GetComponent<MeshRenderer>().material = config.agingMaterials[0];
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
            config.agingMaterials.Length == 0
            || currentAgeIndex >= config.agingMaterials.Length - 1
            || Time.time - timeLastAgeChange < config.agingInterval
        )
        {
            return;
        }

        currentAgeIndex++;
        timeLastAgeChange = Time.time;
        crop.GetComponent<MeshRenderer>().material = config.agingMaterials[currentAgeIndex];
    }
}
