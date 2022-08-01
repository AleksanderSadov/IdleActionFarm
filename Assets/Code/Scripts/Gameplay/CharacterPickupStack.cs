using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CharacterPickupStack : MonoBehaviour
{
    public CharacterPickupConfig config;
    public Transform pickupPivot;

    private void Start()
    {
        InitPickupCollider();
    }

    public IEnumerator Pickup(CropPickup pickup)
    {
        pickup.isPickingUpInProgress = true;
        pickup.GetComponent<Rigidbody>().isKinematic = true;

        bool isPickedUp = false;
        while(!isPickedUp)
        {
            float distance = Vector3.Distance(transform.position, pickup.transform.position);

            if (distance <= config.pickedUpDistance)
            {
                isPickedUp = true;
                Destroy(pickup.gameObject);
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
            if (cropPickup == null || cropPickup.isPickingUpInProgress)
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
