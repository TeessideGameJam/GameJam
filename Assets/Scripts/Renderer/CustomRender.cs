/*
 * Author: Johann
 * --------------
 * This script allows for custom shape rendering.
 * You can specify how many vertices the object has and then move them around as you see fit.
 * This allows you to "draw" whatever shape you wish. (excluding shapes with holes in them; like an A, B, O, e.t.c).
 * 
 * This class uses the custom written PolyRender class that allows for rendering custmo polygons
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CustomRender : MonoBehaviour {

    [Header("Render Inspector-Changes at runtime")]
    public bool debugDrawMode = false; // <-- Will make the program slower if enabled, but is great for visual drawing
    [Header("Number of edges on the shape")]
    public List<Vector3> points = new List<Vector3>(PolyRender.MIN_VERTICES);

    /* --------------------------- */
    private PolyRender mRender = null;

    void Start()
    {
        mRender = new PolyRender(GetComponent<MeshFilter>());

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider)
            meshCollider.sharedMesh = mRender.GetMesh();

        UpdateMeshVertices();
    }

    // Will only update if the user checks DebugDrawMode
    void Update()
    {
        if (debugDrawMode)
        {
            UpdateMeshVertices();
        }
    }

    /* ----------------------------*/

    // This method needs to be called in order to update the mesh.
    // When modifications have been made to the points list, this function needs to be called in order to update it on the screen.
    public void UpdateMeshVertices()
    {
        mRender.vertices = points;
        mRender.UpdateMesh();
    }


}
