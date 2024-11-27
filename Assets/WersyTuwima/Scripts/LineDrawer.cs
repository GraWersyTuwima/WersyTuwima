using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ShirtOutlineChecker _outlineChecker;

    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private float _completionThreshold = 0.9f;

    [SerializeField] private AudioClip _failSound;

    private List<Vector3> _points = new();

    private bool _isValid = true;

    private void Start()
    {
        _lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (_outlineChecker.IsPointValid(mousePosition))
            {
                if (_points.Count == 0 || Vector3.Distance(_points[^1], mousePosition) > _minDistance)
                {
                    _points.Add(mousePosition);
                    _lineRenderer.positionCount = _points.Count;
                    _lineRenderer.SetPosition(_points.Count - 1, mousePosition);
                }
            }
            else
            {
                CheckCompletion();
                ResetDrawing();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_isValid)
            {
                CheckCompletion();
                AudioManager.Instance.PlaySound(_failSound);
            }
            ResetDrawing();
        }
    }

    private void ResetDrawing()
    {
        _points.Clear();
        _lineRenderer.positionCount = 0;
        _isValid = true;
    }

    private void CheckCompletion()
    {
        if (_points.Count < 2) return;

        float totalOutlineLength = CalculateTotalOutlineLength();
        float coveredLength = CalculateCoveredLength();

        float coverage = coveredLength / totalOutlineLength;
        float realCoverage = coverage / 0.065f; // I hate this magic number, but I have no time to make more logical calculations...

        if (realCoverage >= _completionThreshold)
        {
            Debug.Log($"Gratulacje! Obrysowa³eœ koszulkê w {realCoverage * 100:F2}%");
        }
        else
        {
            AudioManager.Instance.PlaySound(_failSound);
        }
    }

    private float CalculateTotalOutlineLength()
    {
        Vector2[] outlinePoints = _outlineChecker.shirtOutline.points;
        float totalLength = 0f;

        for (int i = 0; i < outlinePoints.Length; i++)
        {
            Vector2 current = outlinePoints[i];
            Vector2 next = outlinePoints[(i + 1) % outlinePoints.Length];
            totalLength += Vector2.Distance(current, next);
        }

        return totalLength;
    }

    private float CalculateCoveredLength()
    {
        if (_points.Count < 2) return 0f;

        float coveredLength = 0f;
        for (int i = 0; i < _points.Count - 1; i++)
        {
            Vector2 start = _points[i];
            Vector2 end = _points[i + 1];

            if (_outlineChecker.IsPointValid(start) && _outlineChecker.IsPointValid(end))
            {
                coveredLength += Vector2.Distance(start, end);
            }
        }

        return coveredLength;
    }
}