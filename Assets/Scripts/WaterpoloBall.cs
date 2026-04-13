using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WaterpoloBall : MonoBehaviour
{
    private Rigidbody rb;
    private bool InWater;

    [SerializeField]
    private float upwardsForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (InWater)
        {
            //rb.AddForce(0, upwardsForce, 0);
            rb.useGravity = false;
            transform.position = new(transform.position.x, 3, transform.position.z);
        }
        else
        {
            rb.useGravity=true;
        }
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
