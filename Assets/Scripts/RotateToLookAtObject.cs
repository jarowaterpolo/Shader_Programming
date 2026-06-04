using UnityEngine;
using UnityEngine.UIElements;

public class RotateToLookAtObject : MonoBehaviour
{
    [SerializeField] private Transform ObjectToLookAt;

    private Vector3 lookVector;
    private Vector3 crossVector;

    private float degreesBetweenVectors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ObjectToLookAt == null)
        {
            ObjectToLookAt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        };
    }

    // Update is called once per frame
    void Update()
    {
        lookVector = (ObjectToLookAt.position - transform.position).normalized;
        lookVector.y = 0;
        crossVector = Vector3.Cross(transform.forward, lookVector);
        //Debug.Log(crossVector);

        float dot = Vector3.Dot(transform.forward, lookVector);
        degreesBetweenVectors = Mathf.Acos(Mathf.Clamp(dot,-1f,1f)) * Mathf.Rad2Deg;

        var targetRotation = Quaternion.AngleAxis(degreesBetweenVectors, crossVector.normalized) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
