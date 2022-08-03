using UnityEngine;

public static class Events
{
    public static CropSoldEvent CropSold = new CropSoldEvent();
    public static UpdateMoneyUIEvent UpdateMoneyUIEvent = new UpdateMoneyUIEvent();
}

public class CropSoldEvent : GameEvent
{
    public int sellingCost;
    public GameObject sellPoint;
}

public class UpdateMoneyUIEvent : GameEvent
{
    public int newMoneyValue;
    public GameObject sellPoint;
}
