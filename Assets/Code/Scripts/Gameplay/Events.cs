using UnityEngine;

public static class Events
{
    public static CropSoldEvent CropSold = new CropSoldEvent();
    public static UpdateMoneyUIEvent UpdateMoneyUIEvent = new UpdateMoneyUIEvent();
}

public class CropSoldEvent : GameEvent
{
    public float sellingCost;
    public GameObject sellPoint;
}

public class UpdateMoneyUIEvent : GameEvent
{
    public float newMoneyValue;
    public GameObject sellPoint;
}
