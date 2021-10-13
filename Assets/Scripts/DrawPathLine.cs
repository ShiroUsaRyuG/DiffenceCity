using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathLine : MonoBehaviour
{
    [SerializeField]
    private float startLineWidth = 0.5f;
    [SerializeField]
    private float endLineWidth = 0.5f;

    public void CreatePathLine(Vector3[] drawPaths)
    {
        TryGetComponent(out LineRenderer lineRenderer);

        lineRenderer.startWidth = startLineWidth;
        lineRenderer.endWidth = endLineWidth;

        lineRenderer.positionCount = drawPaths.Length;
        lineRenderer.SetPositions(drawPaths);
    }
}
