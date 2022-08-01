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
        Init();
        InitRandomState();
    }

    private void Update()
    {
        UpdateCropEffects();
    }

    private void Init()
    {
        crop = GetComponent<Crop>();
        config = crop.config;
        originalRotation = transform.rotation;
    }

    private void InitRandomState()
    {
        crop.grownedStateScale = config.grownedStateScale + config.randomScaleDeltaAmplitude * Random.Range(0f, 1f);
        windShakeRandomDelta = Random.Range(config.windShakeMinimumAmplitude, config.windShakeMaximumAmplitude);
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
                transform.rotation = originalRotation;
                break;
            case CropState.Growing:
                transform.rotation = originalRotation;
                break;
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
}
