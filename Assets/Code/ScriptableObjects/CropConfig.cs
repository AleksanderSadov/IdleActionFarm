using UnityEngine;

[CreateAssetMenu(fileName = "CropConfig", menuName = "ScriptableObjects/CropConfig", order = 1)]
public class CropConfig : ScriptableObject
{
    public Vector3 grownedStateScale;
    public Vector3 slicedStateScale;
    public Vector3 gatheredStateScale;
    public Vector3 growingStateScale;
}
