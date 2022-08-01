using UnityEngine;

public static class MiscGizmos
{
    public static void DrawGizmosCircle(Vector3 position, Vector3 normal, float radius, int numberSegments)
    {
        Vector3 temp = (normal.x < normal.z) ? new Vector3(1f, 0f, 0f) : new Vector3(0f, 0f, 1f);
        Vector3 forward = Vector3.Cross(normal, temp).normalized;
        Vector3 right = Vector3.Cross(forward, normal).normalized;

        Vector3 prevPt = position + (forward * radius);
        float angleStep = (Mathf.PI * 2f) / numberSegments;
        for (int i = 0; i < numberSegments; i++)
        {
            float angle = (i == numberSegments - 1) ? 0f : (i + 1) * angleStep;
            Vector3 nextPtLocal = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;
            Vector3 nextPt = position + (right * nextPtLocal.x) + (forward * nextPtLocal.z);

            Gizmos.DrawLine(prevPt, nextPt);

            prevPt = nextPt;
        }
    }
}
