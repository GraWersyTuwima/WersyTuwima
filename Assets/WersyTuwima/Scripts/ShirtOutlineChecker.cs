using UnityEngine;

public class ShirtOutlineChecker : MonoBehaviour
{
    public PolygonCollider2D shirtOutline;
    public float tolerance = 0.5f;

    public bool IsPointValid(Vector2 point)
    {
        Vector2 closestPoint = shirtOutline.ClosestPoint(point);
        return Vector2.Distance(point, closestPoint) <= tolerance;
    }
}