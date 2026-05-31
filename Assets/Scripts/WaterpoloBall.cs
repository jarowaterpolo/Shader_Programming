using System.Collections;
using UnityEngine;

public class WaterpoloBall : MonoBehaviour
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
    private Collider col;

    private GameObject model;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        model = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (InWater)
        {
            rb.mass = 10.0f;
            col.material.bounciness = .5f;
            MoveBallWithWater();
            //Debug.Log("ball bouciness " + col.material.bounciness);
        }
        else
        {
            rb.mass = 1;
            col.material.bounciness = 1;
            //Debug.Log("ball bouciness " + col.material.bounciness);
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
            //Debug.Log(result);
        }

        float pi = 3.14159265359f;
        float wave = Mathf.Sin(Time.timeSinceLevelLoad * pi * WaveSpeed + ((result.x) + (result.z)) * pi * WaveAmount) * Height + yOffset;

        Vector3 pos = transform.position;
        pos.y = wave;

        transform.position = pos;
    }

    public void RespawnBall()
    {
        StartCoroutine(BallRespawn());
    }
    IEnumerator BallRespawn()
    {
        col.enabled = false;
        model.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        transform.position = new(0, 4, 15);
        yield return new WaitForSeconds(1);
        model.SetActive(true);
        col.enabled = true;
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
