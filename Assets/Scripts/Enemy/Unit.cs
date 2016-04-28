using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public Transform target;
    public float speed = 5;
    public bool pathDraw;
    public Color pathCol = Color.black;
    Vector3[] path;
    int tIndex;
	int eSkip;
    private float startTime;
    public float waitTime = .05f;

    void Start()
    {
        PathManager.PathRequest(transform.position, target.position, OnFoundPath);
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime >= waitTime)
        {
            PathManager.PathRequest(transform.position, target.position, OnFoundPath);
            startTime = Time.time;
        }
    }

    public void OnFoundPath(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess && newPath.Length > 0)
        {
            path = newPath;
            StopCoroutine("PathFollow");
            tIndex = 0;
			eSkip=0;
            StartCoroutine("PathFollow");
        }
    }

    IEnumerator PathFollow()
    {
        Vector3 cWaypoint = path[0];
        while (true)
        {
            if (transform.position == cWaypoint)
            {
                tIndex++;
                if (tIndex >= path.Length)
                {
                    yield break;
                }
                cWaypoint = path[tIndex];
            }

			if (tIndex < path.Length-1)
			{
				Vector2 startPos = transform.position;
				Vector2 endPos = path[tIndex++];
				RaycastHit2D checkVisual = Physics2D.Linecast(startPos,endPos,1 << LayerMask.NameToLayer("Unwalkable"));
				Debug.DrawLine(startPos,endPos,Color.green);
				if (checkVisual.collider != null)
				{
					Debug.DrawLine(startPos,checkVisual.point,Color.red);
				}
				else
				{
					cWaypoint = endPos;
				}
			}

            transform.position = Vector3.MoveTowards(transform.position, cWaypoint, speed * Time.deltaTime);
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null && pathDraw)
        {
            for (int i = tIndex; i < path.Length; i++)
            {
                Gizmos.color = pathCol;
                Gizmos.DrawCube(path[i], Vector3.one * 0.5f);
                if (i == tIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
