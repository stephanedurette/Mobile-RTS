using UnityEngine;
using UnityEngine.UI;

public abstract class Action : ScriptableObject
{
    public Sprite ActionIcon;
    public Inventory.ItemValueList ActionCost;

    public abstract void Execute(GameManager gameManager);

    public abstract bool CanPerform(Inventory inventory);
}
