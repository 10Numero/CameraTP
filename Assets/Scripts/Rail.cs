using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Rail : MonoBehaviour
{
    public bool isLoop;
    public float _length;

    [System.Serializable]
    public class Noeud
    {
        public float dist;
        public float normalizedDist;
        public Vector3 noeudPosition;
        [HideInInspector] public Vector3 lastNoeudPosition;
    }

    public List<Noeud> noeuds = new List<Noeud> ();

    [Header("Auto")]
    public bool rIsAuto;
    public Transform target;

    [Header("Debug")]
    public float sphereRadius = 1;
    public Transform spherePosDebug;

    public float sphereDistPosition;

    private void Awake()
    {
        UpdateRailPosition();
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < noeuds.Count; i++)
        {
            Gizmos.DrawLine(noeuds[i].noeudPosition, noeuds[i == noeuds.Count - 1 ? 0 : i + 1].noeudPosition);
            Gizmos.DrawSphere(noeuds[i].noeudPosition, sphereRadius);
        }
    }

    private void Update()
    {
        if (!rIsAuto)
        {
            ApplyPosition(sphereDistPosition);
        }
        B();

        CloseToTarget();
    }

    private void CloseToTarget()
    {
        if (!rIsAuto)
        {
            return;
        }

        Vector3 closestPoint = Vector3.positiveInfinity;
        int segmentIndex = 0;

        for (int i = 0; i < noeuds.Count - 1; i++)
        {
            var a = MathUtils.GetNearestPointOnSegment(noeuds[i].noeudPosition, noeuds[i == noeuds.Count - 1 ? 0 : i + 1].noeudPosition, target.transform.position);
            if (Vector3.Distance(target.transform.position, a) < Vector3.Distance(target.transform.position, closestPoint))
            {
                closestPoint = a;
                segmentIndex = i;
            }
        }
        //GetDistance(closestPoint, segmentIndex);
        ApplyPosition(GetDistance(closestPoint, segmentIndex));
    }

    void B()
    {
        if (!isLoop && sphereDistPosition > noeuds[noeuds.Count - 2].dist && !rIsAuto)
        {
            return;
        }

        for(int i = 0; i < noeuds.Count - 1; i++)
        {
            if (noeuds[i].lastNoeudPosition != transform.GetChild(i).position)
                UpdateRailPosition();

            noeuds[i].lastNoeudPosition = transform.GetChild(i).position;
        }
    }

    public float GetLength()
    {
        return _length;
    }

    public float GetDistance(Vector3 pos, int index = 0)
    {
        float acDist = Vector3.Distance(noeuds[index].noeudPosition, pos);

        float totalLength = noeuds[index].dist + acDist;

        return totalLength;
    }

    [Button]
    public Vector3 GetPosition(float distance)
    {
        if (!isLoop && sphereDistPosition > noeuds[noeuds.Count - 2].dist)
        {
            return Vector3.zero;
        }

        if (distance <= 0)
            return Vector3.zero;

        if (distance > _length)
            distance = distance % _length;

        float normalizedDst = distance / _length;
        var index = ClosestNoeud(normalizedDst);

        float normalizedDstBetweenPoints = noeuds[index == noeuds.Count - 1 ? 0 : index + 1].normalizedDist - noeuds[index].normalizedDist;
        float normalizedDstNoramlizedBetweenPoints = (normalizedDst - noeuds[index].normalizedDist) / normalizedDstBetweenPoints;
        var normalizedPosBetweenPoint = Vector3.Lerp(noeuds[index].noeudPosition, noeuds[index == noeuds.Count - 1 ? 0 : index + 1].noeudPosition, normalizedDstNoramlizedBetweenPoints);
        spherePosDebug.position = normalizedPosBetweenPoint;
        return normalizedPosBetweenPoint;
    }

    public void ApplyPosition(float distance)
    {
        if (!isLoop && sphereDistPosition > noeuds[noeuds.Count - 2].dist)
        {
            return;
        }

        if (distance <= 0)
            return;

        if (distance > _length)
            distance = distance % _length;

        float normalizedDst = distance / _length;
        var index = ClosestNoeud(normalizedDst);

        float normalizedDstBetweenPoints = noeuds[index == noeuds.Count - 1 ? 0 : index + 1].normalizedDist - noeuds[index].normalizedDist;
        float normalizedDstNoramlizedBetweenPoints = (normalizedDst - noeuds[index].normalizedDist) / normalizedDstBetweenPoints;
        var normalizedPosBetweenPoint = Vector3.Lerp(noeuds[index].noeudPosition, noeuds[index == noeuds.Count - 1 ? 0 : index + 1].noeudPosition, normalizedDstNoramlizedBetweenPoints);
        spherePosDebug.position = normalizedPosBetweenPoint; 
    }

    public int ClosestNoeud(float normalizedDst)
    {
        //Debug.Log("noeuds : " + noeuds[i].normalizedPos);
        for (int i = 0; i < noeuds.Count; i++) 
        {
            if (normalizedDst <= noeuds[i].normalizedDist)
                return i - 1;
        }

        Debug.LogWarning("Return null noeud");
        return 0;
    }

    [Button]
    void UpdateRailPosition()
    {
        noeuds.Clear();
        _length = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            Noeud noeud = new Noeud();
            noeud.noeudPosition = transform.GetChild(i).transform.position;
            noeuds.Add(noeud);

            _length += Vector3.Distance(noeuds[i].noeudPosition, noeuds[i == noeuds.Count - 1 ? 0 : i + 1].noeudPosition);
        }

        Noeud cheatNoeud = new Noeud();
        cheatNoeud.noeudPosition = transform.GetChild(0).transform.position;
        cheatNoeud.normalizedDist = 1.0f;
        noeuds.Add(cheatNoeud);
        cheatNoeud.dist = _length;
        
        float actualLength = 0;

        for (int i = 0; i < noeuds.Count - 1; i++)
        {
            if (i > 0)
                actualLength += Vector3.Distance(noeuds[i - 1].noeudPosition, noeuds[i].noeudPosition);

            noeuds[i].dist = actualLength;
            noeuds[i].normalizedDist = actualLength / _length;
        }

    }
}
