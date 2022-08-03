using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig", order = 5)]
public class UIConfig : ScriptableObject
{
    [Tooltip("Duration in seconds at which stack slider animates to new value")]
    public float stackSliderAnimationDuration = 1.0f;
    [Tooltip("Duration in seconds at which floating coin floats to money display")]
    public float floatingCoinAnimationDuration = 1.0f;
    [Tooltip("Duration in seconds at which displayed money count smooth to new value")]
    public float moneyDisplayCountAnimationDuration = 1.0f;
    [Tooltip("Scale multiplier for money count text when money value increments")]
    public float moneyDisplayCountIncrementScale = 1.2f;
}
