using UnityEngine;

[CreateAssetMenu(fileName = "BuildAction", menuName = "Scriptable Objects/Actions/BuildAction")]
public class BuildAction : Action
{
    public override void Execute(GameManager gameManager)
    {
        Debug.Log("Executing");
    }
}
