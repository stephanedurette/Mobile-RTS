using UnityEngine;

public class Action
{
    private ActionModel actionModel;

    public ActionModel ActionModel => actionModel;

    public Action(ActionModel actionModel)
    {
        this.actionModel = actionModel;
    }

    public virtual bool CanExecute(Player player) { return true; }
}
