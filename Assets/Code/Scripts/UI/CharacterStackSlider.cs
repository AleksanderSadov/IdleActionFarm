using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStackSlider : MonoBehaviour
{
    public UIConfig config;
    public TextMeshProUGUI stackCountText;
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
        UpdateStackCountText();
        UpdateStackSliderValue();
    }

    private void UpdateStackCountText()
    {
        stackCountText.text =
            characterPickupStack.CurrentFill.ToString()
            + " / "
            + characterPickupStack.MaxFill.ToString();
    }

    private void UpdateStackSliderValue()
    {
        float newValue = characterPickupStack.FillRatio;

        if (newValue == previousValue)
        {
            return;
        }

        slider.DOValue(newValue, config.stackSliderAnimationDuration);
        previousValue = newValue;
    }
}
