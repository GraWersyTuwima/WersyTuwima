using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float minDistance = 0.1f;

    private List<Vector3> points = new();

    public ShirtOutlineChecker outlineChecker;
    private bool isValid = true;

    void Start()
    {
        lineRenderer.positionCount = 0;

    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (outlineChecker.IsPointValid(mousePosition))
            {
                if (points.Count == 0 || Vector3.Distance(points[^1], mousePosition) > minDistance)
                {
                    points.Add(mousePosition);
                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPosition(points.Count - 1, mousePosition);
                }
            }
            else
            {
                isValid = false;
                ResetDrawing();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isValid)
            {
                CheckCompletion();
            }
            points.Clear();
            lineRenderer.positionCount = 0;
        }
    }

    void ResetDrawing()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    }

    void CheckCompletion()
    {
        Debug.Log("Rysowanie zakoñczone!");
    }
}
