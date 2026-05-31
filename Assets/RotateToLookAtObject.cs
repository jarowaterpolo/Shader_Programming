using UnityEngine;

public class RotateToLookAtObject : MonoBehaviour
{
    [SerializeField] private Transform ObjectToLookAt;

    [SerializeField] private Vector3 lookVector;
    [SerializeField] private Vector3 crossVector;

    [SerializeField] private float degreesBetweenVectors;
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
        lookVector = ObjectToLookAt.position - transform.position;
        crossVector = Vector3.Cross(lookVector, transform.forward);
        degreesBetweenVectors = Mathf.Asin(crossVector.magnitude / lookVector.magnitude * transform.forward.magnitude) * Mathf.Rad2Deg;
    }
}
