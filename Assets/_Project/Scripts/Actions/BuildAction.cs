using UnityEngine;

[CreateAssetMenu(fileName = "BuildAction", menuName = "Scriptable Objects/Actions/BuildAction")]
public class BuildAction : Action
{
    public Sprite PlacementSprite;

    [Header("Grid Placement")]
    public Vector2Int GridSize;

    public override bool CanPerform(Inventory inventory)
    {
        if (ActionCost.Items.Count == 0) return true;

        if(inventory.ContainsItems(ActionCost)) return true;

        return false;
    }

    public override void Execute(GameManager gameManager)
    {
        gameManager.OnBuildActionExecute(this);
    }
}
