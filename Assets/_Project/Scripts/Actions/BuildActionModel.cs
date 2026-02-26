using UnityEngine;

[CreateAssetMenu(fileName = "BuildAction", menuName = "Scriptable Objects/Actions/BuildAction")]
public class BuildActionModel : UnitActionModel
{
    public Sprite PlacementSprite;
    public Vector2Int GridSize;
}
