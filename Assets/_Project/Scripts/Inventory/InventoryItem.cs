using System;

public class InventoryItem : ObservableInt
{
    private InventoryItemModel model;

    public Action<InventoryItemModel, int> OnCountChanged = delegate { };

    public InventoryItemModel Model => model;

    public InventoryItem(InventoryItemModel model, int startingValue) : base(startingValue)
    {
        this.model = model;
        OnValueChanged += (newValue) => OnCountChanged?.Invoke(model, newValue);
    }
}
