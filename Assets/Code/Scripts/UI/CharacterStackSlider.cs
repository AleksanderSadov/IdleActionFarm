using UnityEngine;
using UnityEngine.UI;

public class CharacterStackSlider : MonoBehaviour
{
    public CharacterPickupStack characterPickupStack;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = characterPickupStack.FillRatio;
    }
}
