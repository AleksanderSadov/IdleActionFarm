using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStackSlider : MonoBehaviour
{
    public UIConfig config;
    public CharacterPickupStack characterPickupStack;

    private Slider slider;
    private float previousValue;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
        previousValue = slider.value;
    }

    private void Update()
    {
        float newValue = characterPickupStack.FillRatio;

        if (newValue == previousValue)
        {
            return;
        }

        if (DOTween.IsTweening(slider))
        {
           DOTween.Kill(slider);
        }

        slider.DOValue(newValue, config.stackSliderSpeed);
        previousValue = newValue;
    }
}
