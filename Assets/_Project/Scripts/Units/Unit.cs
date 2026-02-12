using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected SpriteRenderer unitSprite;
    [SerializeField] protected Material selectedMaterial;
    [SerializeField] protected Material defaultMaterial;

    private bool selected;

    public bool Selected
    {
        get { return selected; }
        set { selected = value; unitSprite.material = selected ? selectedMaterial : defaultMaterial; }
    }
}
