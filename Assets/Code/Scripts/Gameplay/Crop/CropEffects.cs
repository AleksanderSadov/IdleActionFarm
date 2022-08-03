using UnityEngine;
using static Crop;

[RequireComponent(typeof(Crop))]
public class CropEffects : MonoBehaviour
{
    public float windShakeRandomDelta;

    private Crop crop;
    private CropConfig config;

    private bool isShakingBackward = true;
    private Quaternion originalRotation;

    private void Start()
    {
        crop = GetComponent<Crop>();
        config = crop.config;
        originalRotation = transform.rotation;

        crop.grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        windShakeRandomDelta = Random.Range(config.windShakeMinimumAmplitude, config.windShakeMaximumAmplitude);
        SetRandomColor();

        crop.cropGathered += OnCropGathered;
        crop.cropDamaged += OnCropDamaged;
    }

    private void Update()
    {
        UpdateCropEffects();
    }

    private void OnDestroy()
    {
        crop.cropGathered -= OnCropGathered;
        crop.cropDamaged -= OnCropDamaged;
    }

    private void UpdateCropEffects()
    {
        switch (crop.state)
        {
            case CropState.Growned:
                ShakeLikeWind();
                break;
            case CropState.Sliced:
                ShakeLikeWind();
                break;
        }
    }

    private void OnCropGathered()
    {
        transform.rotation = originalRotation;
        SetRandomColor();
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

    private void SetRandomColor()
    {
        crop.GetComponent<MeshRenderer>().material = config.materials[Random.Range(0, config.materials.Length)];
    }
}
