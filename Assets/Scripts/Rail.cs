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
        public float normalizedPos;
        public Vector3 noeudPosition;
        [HideInInspector] public Vector3 lastNoeudPosition;
    }

    public List<Noeud> noeuds = new List<Noeud> ();

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
        ApplyPosition(sphereDistPosition);
        B();
    }

    void B()
    {
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

    [Button]
    public Vector3 GetPosition(float distance)
    {
        if (distance <= 0)
            return Vector3.zero;

        if (distance > _length)
            distance = distance % _length;

        float normalizedDst = distance / _length;
        var index = ClosestNoeud(normalizedDst);

        float normalizedDstBetweenPoints = noeuds[index == noeuds.Count - 1 ? 0 : index + 1].normalizedPos - noeuds[index].normalizedPos;
        float normalizedDstNoramlizedBetweenPoints = (normalizedDst - noeuds[index].normalizedPos) / normalizedDstBetweenPoints;
        var normalizedPosBetweenPoint = Vector3.Lerp(noeuds[index].noeudPosition, noeuds[index == noeuds.Count - 1 ? 0 : index + 1].noeudPosition, normalizedDstNoramlizedBetweenPoints);
        spherePosDebug.position = normalizedPosBetweenPoint;
        return normalizedPosBetweenPoint;
    }

    public void ApplyPosition(float distance)
    {
        if (distance <= 0)
            return;

        if (distance > _length)
            distance = distance % _length;

        float normalizedDst = distance / _length;
        var index = ClosestNoeud(normalizedDst);

        float normalizedDstBetweenPoints = noeuds[index == noeuds.Count - 1 ? 0 : index + 1].normalizedPos - noeuds[index].normalizedPos;
        float normalizedDstNoramlizedBetweenPoints = (normalizedDst - noeuds[index].normalizedPos) / normalizedDstBetweenPoints;
        var normalizedPosBetweenPoint = Vector3.Lerp(noeuds[index].noeudPosition, noeuds[index == noeuds.Count - 1 ? 0 : index + 1].noeudPosition, normalizedDstNoramlizedBetweenPoints);
        spherePosDebug.position = normalizedPosBetweenPoint; 
    }

    public int ClosestNoeud(float normalizedDst)
    {
        //Debug.Log("noeuds : " + noeuds[i].normalizedPos);
        for (int i = 0; i < noeuds.Count; i++) 
        {
            if (normalizedDst <= noeuds[i].normalizedPos)
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
        cheatNoeud.normalizedPos = 1.0f;
        noeuds.Add(cheatNoeud);

        float actualLength = 0;

        for (int i = 0; i < noeuds.Count - 1; i++)
        {
            if (i > 0)
                actualLength += Vector3.Distance(noeuds[i - 1].noeudPosition, noeuds[i].noeudPosition);

            noeuds[i].normalizedPos = actualLength / _length;
        }
    }
}
