using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxPoints = 100;
    public float pointSpacing = 0.1f;

    private void Start()
    {
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        DrawTrajectory();
    }

    private void DrawTrajectory()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;

        lineRenderer.positionCount = maxPoints;
        lineRenderer.SetPosition(0, currentPosition);

        for (int i = 1; i < maxPoints; i++)
        {
            float time = i * pointSpacing;
            Vector3 nextPosition = currentPosition + currentVelocity * time;

            lineRenderer.SetPosition(i, nextPosition);

            currentPosition = nextPosition;
            currentVelocity += Physics.gravity * time; // 중력 적용
        }
    }
}
