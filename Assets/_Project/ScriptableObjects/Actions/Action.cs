using UnityEngine;
using UnityEngine.UI;

public abstract class Action : ScriptableObject
{
    public Sprite ActionIcon;
    public abstract void Execute(GameManager gameManager);
}
