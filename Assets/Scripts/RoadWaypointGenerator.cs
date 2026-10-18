using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadWaypointGenerator : MonoBehaviour
{
    // road object to generate waypoints from
    public GameObject roadObject;

    // distance between waypoints
    public float spacing = 10f;

    // height above the road
    public float heightOffset = 0.5f;

    [ContextMenu("Generate Test Waypoints")]
    public void GenerateTestWaypoints()
    {
        // stop if no road is assigned
        if (roadObject == null)
        {
            return;
        }

        // get the road mesh
        MeshFilter filter = roadObject.GetComponent<MeshFilter>();

        // stop if the mesh is missing
        if (filter == null || filter.sharedMesh == null)
        {
            return;
        }

        Vector3[] vertices;

        // get the mesh vertices
        try
        {
            vertices = filter.sharedMesh.vertices;
        }
        catch (UnityException)
        {
            return;
        }

        // make sure the mesh is valid
        if (vertices.Length < 2 || vertices.Length % 2 != 0)
        {
            return;
        }

        // store the center points of the road
        List<Vector3> centers = new List<Vector3>();

        // find the center of each road section
        for (int i = 0; i < vertices.Length; i += 2)
        {
            Vector3 left =
                roadObject.transform.TransformPoint(vertices[i]);

            Vector3 right =
                roadObject.transform.TransformPoint(vertices[i + 1]);

            float width = Vector3.Distance(left, right);

            // stop if the road width is unexpected
            if (width < 7f || width > 11f)
            {
                return;
            }

            centers.Add((left + right) * 0.5f);
        }

        // check that the road is continuous
        for (int i = 1; i < centers.Count; i++)
        {
            float distance = Vector3.Distance(
                centers[i - 1], centers[i]
            );

            if (distance > 15f)
            {
                return;
            }
        }

        // create a parent object for the waypoints
        GameObject container =
            new GameObject("Generated AI Waypoints TEST");

        float distanceSinceLast = spacing;
        Vector3 previous = centers[0];
        int created = 0;

        // create waypoints along the road
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

                // parent the waypoint
                waypoint.transform.SetParent(
                    container.transform
                );

                // place the waypoint above the road
                waypoint.transform.position =
                    centers[i] + Vector3.up * heightOffset;

                created++;
                distanceSinceLast = 0f;
            }

            previous = centers[i];
        }
    }
}