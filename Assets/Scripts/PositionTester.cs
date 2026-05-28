using UnityEngine;

public class PositionTester : MonoBehaviour
{
    [SerializeField] Transform space;
    [SerializeField] float xMultiplier = -0.1f;
    [SerializeField] float zMultiplier = -0.1f;

    [SerializeField] float xOffset = 0.5f;
    [SerializeField] float yOffset = 0f;
    [SerializeField] float zOffset = 0.5f;

    [SerializeField] private int WaveAmount;
    [SerializeField] private float WaveSpeed;
    [SerializeField] private float Height;


    [SerializeField] private bool InWater;

    Vector3 lastResult = Vector3.zero;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InWater)
        {
            rb.mass = 10.0f;
            MoveBallWithWater();
        }
        else
        {
            rb.mass = 1;
        }
    }

    private void MoveBallWithWater()
    {
        Vector3 local = space.InverseTransformPoint(transform.position);
        //Debug.Log(local);

        Vector3 result = Vector3.Scale(local, new Vector3(xMultiplier, 1, zMultiplier)) + new Vector3(xOffset, 0, zOffset);

        if (lastResult != result)
        {
            lastResult = result;
            Debug.Log(result);
        }

        float pi = 3.14159265359f;
        float wave = Mathf.Sin(Time.timeSinceLevelLoad * pi * WaveSpeed + ((result.x) + (result.z)) * pi * WaveAmount) * Height + yOffset;

        Vector3 pos = transform.position;
        pos.y = wave;

        transform.position = pos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            InWater = true;
        }
        else
        {
            InWater = false;
        }
    }
}
