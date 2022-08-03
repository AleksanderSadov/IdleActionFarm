using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float money = 0f;

    private void Start()
    {
        EventManager.AddListener<CropSoldEvent>(OnCropSold);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<CropSoldEvent>(OnCropSold);
    }

    private void OnCropSold(CropSoldEvent evt)
    {
        money += evt.sellingCost;

        UpdateMoneyUIEvent updateMoneyUIEvent = Events.UpdateMoneyUIEvent;
        updateMoneyUIEvent.newMoneyValue = money;
        updateMoneyUIEvent.sellPoint = evt.sellPoint;
        EventManager.Broadcast(updateMoneyUIEvent);
    }
}
