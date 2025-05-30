using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] cellTransforms;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetWorldPos(Vector2Int gridPos)
    {
        int index = gridPos.y * 4 + gridPos.x;
        if (index >= 0 && index < cellTransforms.Length)
            return cellTransforms[index].position;
        else
            return Vector3.zero;
    }

    public bool IsCellOccupied(Vector2Int gridPos)
    {
        foreach (OrangePieceMove piece in FindObjectsOfType<OrangePieceMove>())
        {
            if (piece.gridPos == gridPos)
                return true;
        }
        return false;
    }

    public bool IsInsideGrid(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < 4 && gridPos.y >= 0 && gridPos.y < 4;
    }
}
