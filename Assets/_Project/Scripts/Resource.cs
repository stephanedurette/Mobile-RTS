using System;

public class Resource
{
    public Action<ResourceData, int> OnAmountChanged = delegate { };

    private int amount;
    private ResourceData data;

    public ResourceData Data => data;

    public int Amount
    {
        get { return amount; }
        set
        {
            if (amount == value) return;
            amount = value;
            OnAmountChanged?.Invoke(data, amount);
        }
    }

    public Resource(int startingAmount, ResourceData resourceData)
    {
        amount = startingAmount;
        data = resourceData;
    }
}
