using UnityEngine;

public class BuildAction : UnitAction
{
    private Inventory inventory;

    public Inventory Inventory => inventory;

    public BuildActionModel BuildActionModel => ActionModel as BuildActionModel;

    public BuildAction(UnitActionModel actionModel) : base(actionModel)
    {
        inventory = new();

        inventory.SetValues((actionModel as BuildActionModel).Cost);
    }

    public override bool CanExecute(Player player)
    {
        return player.Inventory.ContainsItems(inventory);
    }

    public override void Execute(GameManager gameManager)
    {
        gameManager.BeginBuildPlacement(this);
    }
}
