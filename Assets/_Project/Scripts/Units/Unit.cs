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
    [SerializeField] private List<UnitActionModel> availableActions;
    [SerializeField] private Player owner;

    public Player Owner => owner;

    private List<UnitAction> actionList;

    public List<UnitAction> ActionList => actionList;

    private bool selected;

    public bool Selected
    {
        get { return selected; }
        set { selected = value; unitSprite.material = selected ? selectedMaterial : defaultMaterial; }
    }

    protected virtual void Awake()
    {
        InitializeActionList();
    }

    private void InitializeActionList()
    {
        ActionFactory actionFactory = new();
        actionList = new List<UnitAction>();
        foreach (var actionModel in availableActions)
        {
            actionList.Add(actionFactory.Create(actionModel));
        }
    }
}
