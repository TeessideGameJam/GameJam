/*
 *  Author: Johann
 *  --------------
 *  This scripts limits the PolyRender class by only allowing basic shapes.
 *  It uses a homebrewed algorithm that makes sure that the shapes are always symmetrical, regardless of what shape it is.
 *  It allows for dynamic shape changing, by updating the public variable numberOfEdges and calling the UpdateShape method.
 *  NOTE: if the numberOfEdges < PolyRender.MIN_VERTICES, then it will clamp to MIN_VERTICES
 */

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ShapeRender : MonoBehaviour
{
    [Header("Render Inspector-Changes at runtime")]
    public bool debugDrawMode = false; // <-- Will make the program slower if enabled, but is great for visual drawing
    
    [Header("Number of edges on the shape")]
    [Range(3, 20)] // <--- is set to be a range, it is not required, however for this project, the minimum/maximum points an enemy can have is 3/20 
    public int numberOfEdges = PolyRender.MIN_VERTICES;

    /* ------------------------------- */

    private PolyRender mRender = null;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mRender = new PolyRender(meshFilter);

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider)
            meshCollider.sharedMesh = mRender.GetMesh();

        UpdateShape();
    }

    // Will only update if the user checks DebugDrawMode
    void Update()
    {
        if (debugDrawMode)
            UpdateShape();
    }

    /* -------------------------- */

    // Will update the shape using the public numberOfEdges.
    // The method will reset the numberOfEdges if it goes below the MIN_VERTICES
    public void UpdateShape()
    {
        if (numberOfEdges < PolyRender.MIN_VERTICES)
            numberOfEdges = PolyRender.MIN_VERTICES;

        mRender.vertices.Clear();

        // determines the angle between each vertex in
        float angleBetweenPoints = 360 / numberOfEdges;
        for(int i = 0; i < numberOfEdges; i++)
        {
            // Calculates the angle of the current vertex
            float newAngle = (angleBetweenPoints * i) * Mathf.Deg2Rad;

            // Calculates the position for the next vertex
            float newPoint_x = Mathf.Cos(newAngle) - Mathf.Sin(newAngle);
            float newPoint_y = Mathf.Sin(newAngle) + Mathf.Cos(newAngle);

            mRender.vertices.Add(new Vector3(newPoint_x, newPoint_y));
        }

        mRender.UpdateMesh();
    }
}
