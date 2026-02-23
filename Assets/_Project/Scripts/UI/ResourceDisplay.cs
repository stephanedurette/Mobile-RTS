using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI costText;

    public void SetResource(ResourceAmount amount)
    {
        image.sprite = amount.Resource.Icon;
        costText.text = amount.Cost.ToString();
    }

    public void SetTextColor(Color color)
    {
        costText.color = color;
    }

    
}
