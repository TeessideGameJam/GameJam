using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(test))]
public class testInspect : Editor {

	// Use this for initialization
    public override void OnInspectorGUI()
    {
        test myTarget = (test)target;

        myTarget.camPos = EditorGUILayout.Vector3Field("Inputs", myTarget.camPos);
        float ans = myTarget.camPos.x * myTarget.camPos.y * myTarget.camPos.z;
        EditorGUILayout.LabelField("Answer", (ans).ToString());
        if (ans < 10 && Application.isPlaying)
        {
            Debug.LogError("Answer must be greater than 10!", target);
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}
