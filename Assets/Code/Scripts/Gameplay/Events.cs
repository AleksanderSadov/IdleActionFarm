public static class Events
{
    public static CropSoldEvent CropSold = new CropSoldEvent();
}

public class CropSoldEvent : GameEvent
{
    public float sellingCost;
}
