using UnityEngine;

public class CharacterPickupStack : MonoBehaviour
{
    public CharacterConfig config;
    public Transform pickupPivot;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        MiscGizmos.DrawGizmosCircle(
            pickupPivot.position,
            new Vector3(0, pickupPivot.position.y, 0),
            config.pickupRadius,
            20
        );
    }
}
