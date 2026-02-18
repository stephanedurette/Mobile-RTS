using UnityEngine;
using UnityEngine.UI;

public abstract class Action : ScriptableObject
{
    public Sprite Image;
    public abstract void Execute();
}
