using UnityEngine;
using static Crop;

[RequireComponent(typeof(Crop))]
public class CropEffects : MonoBehaviour
{
    public float windShakeMinimumAmplitude;
    public float windShakeMaximumAmplitude;
    public float windShakeRandomDelta;

    private Crop crop;
    private CropConfig config;

    private bool isShakingBackward = true;

    private void Start()
    {
        crop = GetComponent<Crop>();
        config = crop.config;
        windShakeMinimumAmplitude = config.windShakeMinimumAmplitude;
        windShakeMaximumAmplitude = config.windShakeMaximumAmplitude;
        windShakeRandomDelta = Random.Range(windShakeMinimumAmplitude, windShakeMaximumAmplitude);
    }

    private void Update()
    {
        UpdateCropEffects();
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
            case CropState.Gathered:
                transform.rotation = crop.originalRotation;
                break;
            case CropState.Growing:
                transform.rotation = crop.originalRotation;
                break;
        }
    }

    private void ShakeLikeWind()
    {
        float deltaAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, crop.originalRotation.eulerAngles.z);

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
}
