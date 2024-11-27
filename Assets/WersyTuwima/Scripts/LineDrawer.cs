using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ShirtOutlineChecker _outlineChecker;

    [SerializeField] private float _minDistance = 0.1f;
    [SerializeField] private float _completionThreshold = 0.9f;

    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioClip _successSound;

    private SewingMinigame _sewingMinigame;
    private List<Vector3> _points = new();

    private bool _isValid = true;

    private void Start()
    {
        _sewingMinigame = transform.parent.GetComponent<SewingMinigame>();
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
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_isValid)
            {
                CheckCompletion();
                return;
            }
            AudioManager.Instance.PlaySound(_failSound);
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
        coverage *= 1.5f;

        if (coverage >= _completionThreshold)
        {
            AudioManager.Instance.PlaySound(_successSound);
            _sewingMinigame.CompleteMinigame();
            enabled = false;
        }
        else
        {
            AudioManager.Instance.PlaySound(_failSound);
            ResetDrawing();
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