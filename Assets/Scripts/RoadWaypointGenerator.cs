
using System.Collections.Generic;
using UnityEngine;

public class RoadWaypointGenerator : MonoBehaviour
{
    public GameObject roadObject;
    public float spacing = 10f;
    public float heightOffset = 0.5f;

    [ContextMenu("Generate Test Waypoints")]
    public void GenerateTestWaypoints()
    {
        if (roadObject == null)
        {
            Debug.LogError("Assign road_0001 first!");
            return;
        }

        MeshFilter filter = roadObject.GetComponent<MeshFilter>();

        if (filter == null || filter.sharedMesh == null)
        {
            Debug.LogError("Road mesh not found!");
            return;
        }

        Vector3[] vertices;

        try
        {
            vertices = filter.sharedMesh.vertices;
        }
        catch (UnityException e)
        {
            Debug.LogError("Cannot read mesh: " + e.Message);
            return;
        }

        if (vertices.Length < 2 || vertices.Length % 2 != 0)
        {
            Debug.LogError("Unexpected road mesh structure.");
            return;
        }

        List<Vector3> centers = new List<Vector3>();

        // Only accept consecutive pairs that resemble road edges.
        for (int i = 0; i < vertices.Length; i += 2)
        {
            Vector3 left =
                roadObject.transform.TransformPoint(vertices[i]);

            Vector3 right =
                roadObject.transform.TransformPoint(vertices[i + 1]);

            float width = Vector3.Distance(left, right);

            if (width < 7f || width > 11f)
            {
                Debug.LogWarning(
                    "Unexpected vertex pair width at index " + i +
                    ": " + width
                );
                return;
            }

            centers.Add((left + right) * 0.5f);
        }

        // Check that centers follow a continuous route.
        for (int i = 1; i < centers.Count; i++)
        {
            float distance = Vector3.Distance(
                centers[i - 1], centers[i]
            );

            if (distance > 15f)
            {
                Debug.LogWarning(
                    "Road vertex order is discontinuous at " + i +
                    ". No waypoints generated."
                );
                return;
            }
        }

        GameObject container =
            new GameObject("Generated AI Waypoints TEST");

        float distanceSinceLast = spacing;
        Vector3 previous = centers[0];
        int created = 0;

        for (int i = 0; i < centers.Count; i++)
        {
            if (i > 0)
            {
                distanceSinceLast += Vector3.Distance(
                    previous, centers[i]
                );
            }

            if (distanceSinceLast >= Mathf.Max(1f, spacing))
            {
                GameObject waypoint = new GameObject(
                    "Auto Waypoint " + (created + 1)
                );

                waypoint.transform.SetParent(
                    container.transform
                );

                waypoint.transform.position =
                    centers[i] + Vector3.up * heightOffset;

                created++;
                distanceSinceLast = 0f;
            }

            previous = centers[i];
        }

        Debug.Log(
            "Generated " + created +
            " test waypoints. Inspect them before using!"
        );
    }
}
