using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI costText;

    public ResourceData ResourceData { get; private set; }

    public void SetAmount(int amount)
    {
        costText.text = amount.ToString();
    }

    public void SetResource(ResourceData resource)
    {
        image.sprite = resource.Icon;
        ResourceData = resource;
    }

    public void SetTextColor(Color color)
    {
        costText.color = color;
    }

    
}
