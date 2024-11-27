using UnityEngine;

public class ShirtOutlineChecker : MonoBehaviour
{
    public PolygonCollider2D shirtOutline;

    public bool IsPointValid(Vector2 point)
    {
        Vector2 closestPoint = shirtOutline.ClosestPoint(point);
        return Vector2.Distance(point, closestPoint) < 0.05f;
    }
}