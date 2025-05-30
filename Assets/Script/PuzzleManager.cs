using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float tileSize = 1.5f;

    private OrangePieceController[,] grid;
    public bool[,] blockedCells;

    public GameObject blockerPrefab;

    [Header("Blocked Positions (x, y)")]
    public List<Vector2Int> blockedPositions = new List<Vector2Int>();

    private void Awake()
    {
        grid = new OrangePieceController[gridWidth, gridHeight];
        blockedCells = new bool[gridWidth, gridHeight];

        foreach (var pos in blockedPositions)
        {
            if (IsValidPosition(pos.x, pos.y))
            {
                blockedCells[pos.x, pos.y] = true;
            }
        }

        float offsetX = -(gridWidth - 1) * tileSize / 2f;
        float offsetY = -(gridHeight - 1) * tileSize / 2f;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (blockedCells[x, y] && blockerPrefab != null)
                {
                    Vector3 pos = new Vector3(x * tileSize + offsetX, y * tileSize + offsetY, 0);
                    Instantiate(blockerPrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }

    public void RegisterPiece(OrangePieceController piece)
    {
        if (IsValidPosition(piece.gridX, piece.gridY) && !blockedCells[piece.gridX, piece.gridY])
        {
            grid[piece.gridX, piece.gridY] = piece;
        }
    }

    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }

    public bool IsCellEmpty(int x, int y)
    {
        if (!IsValidPosition(x, y)) return false;
        if (blockedCells[x, y]) return false;
        return grid[x, y] == null;
    }

    public void MovePiece(OrangePieceController piece, int fromX, int fromY, int toX, int toY)
    {
        grid[fromX, fromY] = null;
        grid[toX, toY] = piece;
    }

    public void CheckWin()
    {
        for (int x = 0; x < gridWidth - 1; x++)
        {
            for (int y = 0; y < gridHeight - 1; y++)
            {
                var a = grid[x, y];
                var b = grid[x + 1, y];
                var c = grid[x, y + 1];
                var d = grid[x + 1, y + 1];

                if (a != null && b != null && c != null && d != null)
                {
                    Debug.Log($"Checking at ({x},{y}): {a.pieceID}, {b.pieceID}, {c.pieceID}, {d.pieceID}");

                    if (a.pieceID == 3 && b.pieceID == 4 && c.pieceID == 1 && d.pieceID == 2)
                    {
                        SceneManager.LoadScene("Win");
                        return;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float offsetX = -(gridWidth - 1) * tileSize / 2f;
                float offsetY = -(gridHeight - 1) * tileSize / 2f;
                Vector3 pos = new Vector3(x * tileSize + offsetX, y * tileSize + offsetY, 0);
                Gizmos.DrawWireCube(pos, Vector3.one * tileSize);
            }
        }
    }
}
