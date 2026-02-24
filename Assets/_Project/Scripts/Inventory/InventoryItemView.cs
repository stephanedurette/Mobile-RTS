using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI costText;

    private InventoryItem item;

    public void Bind(InventoryItem item)
    {
        Unbind();

        this.item = item;
        image.sprite = item.Model.Icon;
        costText.text = item.Value.ToString();
        item.OnCountChanged += OnCountChanged;
    }

    public void Unbind()
    {
        if (item != null)
            item.OnCountChanged -= OnCountChanged;

        item = null;
    }

    private void OnCountChanged(InventoryItemModel model, int newValue)
    {
        costText.text = newValue.ToString();
    }

    
}
