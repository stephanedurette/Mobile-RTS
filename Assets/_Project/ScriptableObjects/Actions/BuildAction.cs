using UnityEngine;

[CreateAssetMenu(fileName = "BuildAction", menuName = "Scriptable Objects/Actions/BuildAction")]
public class BuildAction : Action
{
    public Sprite PlacementSprite;

    [Header("Grid Placement")]
    public Vector2Int GridSize;

    [Header("Resource Cost")]
    public ResourceAmount[] ResourceCosts;

    public override void Execute(GameManager gameManager)
    {
        gameManager.OnBuildActionExecute(this);
    }
}
