/*
 * Author: Sam & Johann
 * -------
 * This class is a custom inspector for the CustomRender class.
 * It converts the public Vector3 list variable (the mesh vertices), into a:
 *  list of Vector2s, where the x and y values are clamped to -1 and 1, using sliders.
 *  
 * Since the custom inspector overrides the original inspector. The debugDrawMode has been implemented to work here as well.
 */

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


[CustomEditor(typeof(CustomRender))]
public class InspectorShapeRender : Editor 
{
    private int numElements;
    private bool debugDraw;

    private const int MIN_POINTS = 3;
    private const float MIN_VERTEX_POINT = -1f;
    private const float MAX_VERTEX_POINT = 1f;

    public override void OnInspectorGUI()
    {
        CustomRender myTarget = (CustomRender)target;

        // debugDrawMode section
        EditorGUILayout.LabelField("Render Inspector-Changes at runtime", EditorStyles.boldLabel);
        myTarget.debugDrawMode = EditorGUILayout.Toggle("Debug Draw:", myTarget.debugDrawMode);

        // point (list) section
        EditorGUILayout.LabelField("────────.─────────", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Number of Vertices", EditorStyles.boldLabel);
        numElements = EditorGUILayout.IntField("Size:", myTarget.points.Count);

        // the number of elements cannot be less than MIN_POINTS
        if (numElements < MIN_POINTS) 
            numElements = MIN_POINTS;

        /* if the number of elements is greater than the current vertex list
         * Then add new default values */
        if (numElements > myTarget.points.Count)
        {
            int elementsToAdd = numElements - myTarget.points.Count;
            for (int i = 0; i < elementsToAdd; i++)
                myTarget.points.Add(new Vector3(0, 0, 0));
        }
            /* if the number of elements is less than the current vertex list
             * Then remove the tail of the list until they are equal */
        else if(numElements < myTarget.points.Count)
        {
            for(int i = myTarget.points.Count; i > numElements; i--)
                myTarget.points.RemoveAt(numElements);
        }

        // UI layout values
        const int labelWidth = 15;
        float sliderWidth = EditorGUIUtility.currentViewWidth / 2.5f;

        // Iterate over all the values in the points lists
        for (int i = 0; i < myTarget.points.Count; i++)
        {
            EditorGUILayout.TextField("Vertex Position " + i.ToString(), EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("X: ", GUILayout.Width(EditorGUIUtility.labelWidth = labelWidth));
            float x = EditorGUILayout.Slider(myTarget.points[i].x, MIN_VERTEX_POINT, MAX_VERTEX_POINT, GUILayout.Width(sliderWidth));
            EditorGUILayout.LabelField("Y: ", GUILayout.Width(EditorGUIUtility.labelWidth = labelWidth));
            float y = EditorGUILayout.Slider(myTarget.points[i].y, MIN_VERTEX_POINT, MAX_VERTEX_POINT, GUILayout.Width(sliderWidth));
            EditorGUILayout.EndHorizontal();
            
            // convert the values into a Vector3 (which points is)
            myTarget.points[i] = new Vector3(x, y, 0);
        }
    }

}
