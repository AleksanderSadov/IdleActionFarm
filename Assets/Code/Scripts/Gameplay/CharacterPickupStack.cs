using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickupStack : MonoBehaviour
{
    public CharacterPickupConfig config;
    public Transform pickupPivot;
    public List<CropPickup> pickupsStack = new List<CropPickup>();

    private void Start()
    {
        InitPickupCollider();
    }

    public IEnumerator Pickup(CropPickup pickup)
    {
        pickupsStack.Add(pickup);
        pickup.isPickingUpInProgress = true;
        pickup.GetComponent<Rigidbody>().isKinematic = true;

        bool isPickedUp = false;
        while(!isPickedUp)
        {
            float distance = Vector3.Distance(transform.position, pickup.transform.position);

            if (distance <= config.pickedUpDistance)
            {
                isPickedUp = true;
                pickup.gameObject.transform.parent = transform;
                pickup.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                pickup.transform.position = Vector3.MoveTowards(
                    pickup.transform.position,
                    transform.position,
                    config.poolUpSpeed
                );
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        MiscGizmos.DrawGizmosCircle(
            pickupPivot.position,
            new Vector3(0, pickupPivot.position.y, 0),
            config.poolUpRadius,
            20
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropPickup"))
        {
            CropPickup cropPickup = other.GetComponent<CropPickup>();
            if (
                pickupsStack.Count >= config.maxStackSize
                || cropPickup == null
                || cropPickup.isPickingUpInProgress
            )
            {
                return;
            }

            StartCoroutine(Pickup(cropPickup));
        }
    }

    private void InitPickupCollider()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.center = pickupPivot.position - transform.position;
        sphereCollider.radius = config.poolUpRadius;
    }
}
