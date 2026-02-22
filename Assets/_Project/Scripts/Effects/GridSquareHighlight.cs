using UnityEngine;

public class GridSquareHighlight : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float alpha;

    public Color HighlightColor
    {
        get { return spriteRenderer.material.color; }
        set { spriteRenderer.material.color = new Color(value.r, value.g, value.b, alpha); }
    }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
    }
}
