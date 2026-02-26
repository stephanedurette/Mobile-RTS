using UnityEngine;

public class UnitAction
{
    private UnitActionModel actionModel;

    public UnitActionModel ActionModel => actionModel;

    public UnitAction(UnitActionModel actionModel)
    {
        this.actionModel = actionModel;
    }

    public virtual bool CanExecute(Player player) { return true; }

    public virtual void Execute(GameManager gameManager) { 
        
    }
}
