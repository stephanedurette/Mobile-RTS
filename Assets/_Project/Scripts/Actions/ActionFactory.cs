using UnityEngine;

public class ActionFactory
{
    public ActionFactory()
    {

    }

    public Action Create(ActionModel actionModel)
    {
        switch (actionModel)
        {
            case BuildActionModel bAM:
                return new BuildAction(bAM);
            case ActionModel aM:
                return new Action(aM);
            default:
                return null;
        }
    }
}
