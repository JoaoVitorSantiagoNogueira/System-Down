using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public List<Transform> points = new();

    public bool loop = true;

    private readonly List<float> segmentLengths = new();

    public float TotalLength { get; private set; }

    void Awake()
    {
        Recalculate();
    }

    public void Recalculate()
    {
        segmentLengths.Clear();
        TotalLength = 0;

        if (points.Count < 2)
            return;

        int count = loop ? points.Count : points.Count - 1;

        for (int i = 0; i < count; i++)
        {
            Vector3 a = points[i].position;
            Vector3 b = points[(i + 1) % points.Count].position;

            float length = Vector3.Distance(a, b);

            segmentLengths.Add(length);
            TotalLength += length;
        }
    }

        public Vector3 GetPosition(float distance)
    {
        if (points.Count == 0)
            return transform.position;

        if (points.Count == 1)
            return points[0].position;

        if (loop)
            distance = Mathf.Repeat(distance, TotalLength);
        else
        {
            float cycle = TotalLength * 2f;

            distance = Mathf.Repeat(distance, cycle);
    
            if (distance > TotalLength)
                distance = cycle - distance;
            }

        float accumulated = 0;

        for (int i = 0; i < segmentLengths.Count; i++)
        {
            float next = accumulated + segmentLengths[i];

            if (distance <= next)
            {
                float t = (distance - accumulated) / segmentLengths[i];

                Vector3 a = points[i].position;
                Vector3 b = points[(i + 1) % points.Count].position;

                return Vector3.Lerp(a, b, t);
            }

            accumulated = next;
        }

        return points[^1].position;
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (points.Count < 2)
            return;

        int count = loop ? points.Count : points.Count - 1;

        for (int i = 0; i < count; i++)
        {
            Vector3 a = points[i].position;
            Vector3 b = points[(i + 1) % points.Count].position;

            Gizmos.DrawLine(a, b);
            Gizmos.DrawSphere(a, 0.1f);
        }

        if (!loop)
            Gizmos.DrawSphere(points[^1].position, 0.1f);
    }
}