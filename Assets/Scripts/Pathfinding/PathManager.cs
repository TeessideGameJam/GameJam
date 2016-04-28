using UnityEngine;
using System.Collections.Generic;
using System;


public class PathManager : MonoBehaviour {

    struct RequestPath
    {
        public Vector3 pStart;
        public Vector3 pEnd;
        public Action<Vector3[], bool> back;

        public RequestPath(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            pStart = start;
            pEnd = end;
            back = callback;
        }
    }

    Queue<RequestPath> pathQueue = new Queue<RequestPath>();
    RequestPath cPRequest;

    static PathManager inst;
    Pathfinding pathfind;
    bool isProcessing;

    void Awake()
    {
        inst = this;
        pathfind = GetComponent<Pathfinding>();
    }

	public static void PathRequest(Vector3 pStart, Vector3 pEnd, Action<Vector3[], bool> back)
    {
        RequestPath newReq = new RequestPath(pStart, pEnd, back);
        inst.pathQueue.Enqueue(newReq);
        inst.TryNextProcess();
    }

    void TryNextProcess()
    {
        if (!isProcessing && pathQueue.Count > 0)
        {
            cPRequest = pathQueue.Dequeue();
            isProcessing = true;
            pathfind.StartPathFind(cPRequest.pStart, cPRequest.pEnd);
        }
    }

    public void PathProcessFinished(Vector3[] path, bool success)
    {
        cPRequest.back(path, success);
        isProcessing = false;
        TryNextProcess();
    }
}
