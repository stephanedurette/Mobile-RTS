using UnityEngine;

public class ActionFactory
{
    public ActionFactory()
    {

    }

    public UnitAction Create(UnitActionModel actionModel)
    {
        switch (actionModel)
        {
            case BuildActionModel bAM:
                return new BuildAction(bAM);
            case UnitActionModel aM:
                return new UnitAction(aM);
            default:
                return null;
        }
    }
}
