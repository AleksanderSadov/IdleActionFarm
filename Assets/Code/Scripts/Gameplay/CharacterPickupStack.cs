using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickupStack : MonoBehaviour
{
    public CharacterPickupConfig config;
    public Transform pickupPivot;
    public Transform pickupBag;
    public List<CropPickup> pickupStack = new List<CropPickup>();

    public bool IsFull => pickupStack.Count >= config.maxStackSize;
    public float FillRatio => (float) pickupStack.Count / config.maxStackSize;

    private Vector3 pickupBagOriginalScale;
    private bool isInsideSellPointTrigger = false;

    private void Start()
    {
        InitPickupCollider();
        pickupBagOriginalScale = pickupBag.transform.localScale;
        UpdatePickupBagScale();
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
                pickupStack.Count >= config.maxStackSize
                || cropPickup == null
                || cropPickup.isPickingUpInProgress
                || cropPickup.isSellingInProgress
            )
            {
                return;
            }

            StartCoroutine(Pickup(cropPickup));
        }

        if (other.CompareTag("SellPoint"))
        {
            if (pickupStack.Count == 0)
            {
                return;
            }

            isInsideSellPointTrigger = true;
            StartCoroutine(SellCurrentPickups(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SellPoint"))
        {
            isInsideSellPointTrigger = false;
        }
    }

    private IEnumerator Pickup(CropPickup pickup)
    {
        pickupStack.Add(pickup);
        pickup.isPickingUpInProgress = true;
        pickup.GetComponent<Rigidbody>().isKinematic = true;

        float distance;
        do
        {
            pickup.transform.position = Vector3.MoveTowards(
                pickup.transform.position,
                transform.position,
                config.poolUpSpeed
            );

            distance = Vector3.Distance(transform.position, pickup.transform.position);

            yield return new WaitForFixedUpdate();
        } while (distance > config.pickedUpDistance);

        pickup.gameObject.transform.parent = transform;
        pickup.gameObject.GetComponent<MeshRenderer>().enabled = false;
        UpdatePickupBagScale();
    }

    private IEnumerator SellCurrentPickups(GameObject sellPoint)
    {
        while (pickupStack.Count > 0 && isInsideSellPointTrigger)
        {
            CropPickup pickup = pickupStack[pickupStack.Count - 1];
            pickupStack.Remove(pickup);
            UpdatePickupBagScale();
            StartCoroutine(SellSinglePickup(pickup, sellPoint));

            yield return new WaitForSeconds(config.sellingDelay);
        }
    }

    private IEnumerator SellSinglePickup(CropPickup pickup, GameObject sellPoint)
    {
        pickup.isSellingInProgress = true;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<MeshRenderer>().enabled = true;
        pickup.gameObject.transform.parent = null;

        float distance;
        do
        {
            pickup.transform.position = Vector3.MoveTowards(
                pickup.transform.position,
                sellPoint.transform.position,
                config.sellingFlySpeed
            );

            distance = Vector3.Distance(pickup.transform.position, sellPoint.transform.position);

            yield return new WaitForFixedUpdate();
        } while (distance > 1.5f);

        CropSoldEvent cropSoldEvent = Events.CropSold;
        cropSoldEvent.sellingCost = pickup.cropSellingCostConfig.sellingCost;
        cropSoldEvent.sellPoint = sellPoint;
        EventManager.Broadcast(cropSoldEvent);

        Destroy(pickup.gameObject);
    }

    private void InitPickupCollider()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.center = pickupPivot.position - transform.position;
        sphereCollider.radius = config.poolUpRadius;
    }

    private void UpdatePickupBagScale()
    {
        pickupBag.GetComponent<MeshRenderer>().enabled = pickupStack.Count > 0;

        pickupBag.transform.localScale = new Vector3(
            pickupBagOriginalScale.x * pickupStack.Count / config.maxStackSize,
            pickupBagOriginalScale.y,
            pickupBagOriginalScale.z
        );
    }
}
