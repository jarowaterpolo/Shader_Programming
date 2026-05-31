using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private Transform WaterpoloBall;
    [SerializeField] private UnityEvent unityEvent;

    private Vector3 VectorToBall;
    private float dotProductValue;

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.right * 5, Color.magenta);

        VectorToBall = WaterpoloBall.position - transform.position;
        //Debug.Log(VectorToBall);
        Debug.DrawRay(transform.position, VectorToBall, Color.red);

        dotProductValue =  Vector3.Dot(transform.forward, VectorToBall);

        if (dotProductValue < 1.5f && VectorToBall.magnitude < 5)
        {
            unityEvent?.Invoke();
        }
    }
}
