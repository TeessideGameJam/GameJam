/*
 * Author: Johann
 * --------------
 * This class is responsible for allowing any custom mesh rendering.
 * It relies on MeshFilters to generate a new Mesh with any given properties.
 * 
 * This class is a very basic 2d renderer. It does not support bump maps  
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolyRender
{
    public static int MIN_VERTICES = 3;

    public List<Vector3> vertices;

    /* -------------------------- */
    private List<Vector2> mTextCoords = null;
    private List<Vector3> mNormals = null;
    private List<Vector4> mTangents = null; // <--- not used (not using bump maps) set to 0

    private Mesh mMesh  = null;
    private MeshFilter mMeshFilter = null;

    private void UpdateTextureCoords()
    {
        mTextCoords.Clear();
        for (int i = 0; i < vertices.Count; i++)
            mTextCoords.Add(new Vector2(vertices[i].x, vertices[i].z));
    }

    // Normals are very simple for this project, using static normals (pointing out of the screen)
    private void UpdateNormals()
    {
        mNormals.Clear();
        for (int i = 0; i < vertices.Count; i++)
            mNormals.Add(Vector3.back);
    }

    // Tangents are not used, but to stop the Unity debug spam, they have been generated with a zero vec4 value
    private void UpdateTangents()
    {
        mTangents.Clear();
        for (int i = 0; i < vertices.Count; i++)
            mTangents.Add(Vector4.zero);
    }

    /* --------------------------- */
    /* +=== Constructor ===+ */
    public PolyRender(MeshFilter meshFilter)
    {
        vertices = new List<Vector3>();
        mTextCoords = new List<Vector2>();
        mNormals = new List<Vector3>();

        mTangents = new List<Vector4>(); // <--- not used 

        // Mesh init
        mMesh = new Mesh();
        mMeshFilter = meshFilter;
        mMeshFilter.mesh = mMesh;
        mMesh.name = "Custom Poly Mesh";

        UpdateMesh();
    }

    /* +==== Mesh Updater ====+ */
    // This needs to be called in order to update the mesh
    // *NOTE : try to use this as little as possible. Only update the mesh when needs be
    public void UpdateMesh()
    {
        mMesh.Clear();
        Triangulator elements = new Triangulator(vertices);

        UpdateTextureCoords();
        UpdateNormals();
        UpdateTangents();

        mMesh.vertices = vertices.ToArray();
        mMesh.triangles = elements.Triangulate();
        mMesh.uv = mTextCoords.ToArray();
        mMesh.normals = mNormals.ToArray();
        mMesh.tangents = mTangents.ToArray();
        mMesh.RecalculateNormals();
        mMesh.RecalculateBounds();
    }
}
