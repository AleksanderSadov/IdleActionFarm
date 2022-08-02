using UnityEngine;

[CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig", order = 5)]
public class UIConfig : ScriptableObject
{
    [Tooltip("Speed in seconds at which stack slider animates to new value")]
    public float stackSliderSpeed = 1.0f;
}
