using UnityEngine;

public class GameController : MonoBehaviour
{
    public OrangePieceMove targetPiece;

    public void MoveUp() => targetPiece.TryMove(Vector2Int.up);
    public void MoveDown() => targetPiece.TryMove(Vector2Int.down);
    public void MoveLeft() => targetPiece.TryMove(Vector2Int.left);
    public void MoveRight() => targetPiece.TryMove(Vector2Int.right);
}
