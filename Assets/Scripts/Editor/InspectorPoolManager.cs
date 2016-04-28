using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(PoolManager))]
public class InspectorPoolManager : Editor
{
    private int numElements;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
   
        PoolManager myTarget = (PoolManager)target;

        numElements = EditorGUILayout.IntField("Size: ", myTarget.pooledObjects.Count);
        /* if the number of elements is greater than the current vertex list
         * Then add new default values */
        if (numElements > myTarget.pooledObjects.Count)
        {
            int elementsToAdd = numElements - myTarget.pooledObjects.Count;
            for (int i = 0; i < elementsToAdd; i++)
                myTarget.pooledObjects.Add(new PoolObject());
        }
        /* if the number of elements is less than the current vertex list
         * Then remove the tail of the list until they are equal */
        else if (numElements < myTarget.pooledObjects.Count)
        {
            for (int i = myTarget.pooledObjects.Count; i > numElements; i--)
                myTarget.pooledObjects.RemoveAt(numElements);
        }

        EditorGUI.indentLevel = 1;
        float intFieldWidth = EditorGUIUtility.currentViewWidth / 2.5f;
        const float labelWidth = 70;
        for(int i = 0; i < myTarget.pooledObjects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            myTarget.pooledObjects[i].obj = EditorGUILayout.ObjectField("", myTarget.pooledObjects[i].obj, 
                                                                            typeof(GameObject), 
                                                                            GUILayout.Width(intFieldWidth)) 
                                                                            as GameObject;
            EditorGUILayout.LabelField("Amount:", GUILayout.Width(labelWidth));
            myTarget.pooledObjects[i].amount = EditorGUILayout.IntField(myTarget.pooledObjects[i].amount, GUILayout.Width(labelWidth));
            EditorGUILayout.EndHorizontal();
        }
    }
}
