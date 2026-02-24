using System;

public class Resource
{
    public Action<InventoryItemModel, int> OnAmountChanged = delegate { };

    private int amount;
    private InventoryItemModel data;

    public InventoryItemModel Data => data;

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

    public Resource(int startingAmount, InventoryItemModel resourceData)
    {
        amount = startingAmount;
        data = resourceData;
    }
}
