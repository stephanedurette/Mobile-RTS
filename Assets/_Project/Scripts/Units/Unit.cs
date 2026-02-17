using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected SpriteRenderer unitSprite;
    [SerializeField] protected Material selectedMaterial;
    [SerializeField] protected Material defaultMaterial;

    [Header("Settings")]
    [SerializeField] private List<Action> actions;

    public List<Action> Actions => actions;

    private bool selected;

    public bool Selected
    {
        get { return selected; }
        set { selected = value; unitSprite.material = selected ? selectedMaterial : defaultMaterial; }
    }
}
