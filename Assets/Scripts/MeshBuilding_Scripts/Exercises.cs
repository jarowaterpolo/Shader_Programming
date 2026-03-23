using System.Collections.Generic;
using UnityEngine;

public class Exercises : MonoBehaviour 
{
    public Vector3 F = new(0.6f, 0, 0.8f);
    public Vector3 U = new(0, 1, 0);
    [Space(20)]
    [SerializeField]
    private Vector3 R;
    [Space(20)]
    public List<Vector3> Points = new();
    public List<Vector3> Vectors = new();

    public void Update()
    {
        Vector3 P1;
        Vector3 P2;
        Vector3 P3;

        if (Points.Count > 0)
        {
            P1 = Points[0];
            P2 = Points[1];
            P3 = Points[2];

            Points.Clear();
            Vectors.Clear();

            Points.Add(P1);
            Points.Add(P2);
            Points.Add(P3);

            var P4 = new Vector3((P1.x + P2.x + P3.x) / 3, (P1.y + P2.y + P3.y) / 3, (P1.z + P2.z + P3.z) / 3);

            Points.Add(P4);

            R = Vector3.Cross(F, U);

            Vectors.Add(Points[1] - Points[0]);
            Vectors.Add(Points[2] - Points[1]);
            Vectors.Add(Points[0] - Points[2]);

            Vectors.Add(Vector3.Cross(Vectors[0], Vectors[1]));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, F);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, U);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, R);

        Gizmos.color = Color.green;
        foreach (var point in Points) 
        {
            Gizmos.DrawSphere(transform.position + point, .1f);
            //Gizmos.DrawRay(transform.position + point, point);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < Vectors.Count; i++)
        {
            Gizmos.DrawRay(transform.position + Points[i], Vectors[i]);
        }
    }
}
