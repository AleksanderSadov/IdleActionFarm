using UnityEngine;

public class SellPointDirectionArrow : MonoBehaviour
{
    public CharacterPickupStack characterPickupStack;
    public Transform sellPoint;

    private MeshRenderer arrowMesh;

    private void Start()
    {
        arrowMesh = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (characterPickupStack.IsFull)
        {
            arrowMesh.enabled = true;
            transform.LookAt(sellPoint.position);
        }
        else
        {
            arrowMesh.enabled = false;
        }
    }
}
