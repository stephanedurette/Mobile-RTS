using UnityEngine;

public class PlacementCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer placementImage;

    private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; placementImage.sprite = sprite; }
    }
}
